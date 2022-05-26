using CVPZ.Application.Job.Events;
using CVPZ.Infrastructure.Data;
using MediatR;

namespace CVPZ.Application.Job;

public static class CreateJob
{

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly CVPZContext _context;
        private readonly IMediator _mediator;

        public Handler(CVPZContext context, IMediator mediator)
        {
            this._context = context;
            this._mediator = mediator;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var entity = await MapToEntity(request);
            await PersistEntity(entity);

            await _mediator.Publish(new JobCreated());

            return new Response(entity.Id.ToString(),
                entity.Title,
                entity.EmployerName,
                entity.Description,
                entity.StartDate,
                entity.EndDate);
        }

        private async Task<Domain.Job> MapToEntity(Request request)
        {
            return new Domain.Job
            {
                Title = request.Title,
                EmployerName = request.EmployerName,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
        }

        private async Task PersistEntity(Domain.Job entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

    public record Request (
        string Title,
        string EmployerName,
        string? Description,
        DateTimeOffset StartDate,
        DateTimeOffset? EndDate)
        : IRequest<Response>;

    public record Response (
        string Id,
        string EmployerName,
        string Title,
        string? Description,
        DateTimeOffset StartDate,
        DateTimeOffset? EndDate);
}
