using CVPZ.Application.Job.Events;
using CVPZ.Core;
using CVPZ.Infrastructure.Data;
using MediatR;
using OneOf;

namespace CVPZ.Application.Job;

public static class CreateJob
{
    public class Errors
    {
        public static Error JobTitleRequired => new(Code: nameof(JobTitleRequired), "Title is required");
    }

    public class Handler : IRequestHandler<Request, OneOf<Response, Error>>
    {
        private readonly CVPZContext _context;
        private readonly IMediator _mediator;

        public Handler(CVPZContext context, IMediator mediator)
        {
            this._context = context;
            this._mediator = mediator;
        }

        public async Task<OneOf<Response, Error>> Handle(Request request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return Errors.JobTitleRequired;

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
        : IRequest<OneOf<Response, Error>>;

    public record Response (
        string Id,
        string EmployerName,
        string Title,
        string? Description,
        DateTimeOffset StartDate,
        DateTimeOffset? EndDate);
}
