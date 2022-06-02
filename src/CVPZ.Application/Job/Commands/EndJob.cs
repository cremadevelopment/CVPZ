using CVPZ.Infrastructure.Data;
using MediatR;
using Serilog;
using static CVPZ.Application.Job.JobEvents;

namespace CVPZ.Application.Job.Commands;

public static class EndJob
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly CVPZContext _context;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public Handler(CVPZContext context, IMediator mediator, ILogger logger)
        {
            this._context = context;
            this._mediator = mediator;
            this._logger = logger;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var validId = int.TryParse(request.JobId, out int jobId);
            if (!validId)
            {
                _logger.Error("Job id of {jobId} was invalid.", request.JobId);
                throw new KeyNotFoundException($"Job id of {request.JobId} was invalid.");
            }

            var entity = await _context.Jobs.FindAsync(jobId);
            if (entity == null)
            {
                _logger.Error("Job with id {jobId} not found.", jobId);
                throw new KeyNotFoundException($"Job with id {jobId} not found.");
            }

            entity.EndDate = request.EndDate;
            await _context.SaveChangesAsync();

            await _mediator.Publish(new JobEnded(request.JobId));

            return new Response(jobId.ToString());
        }
    }

    public record Request(string JobId, DateTimeOffset EndDate) : IRequest<Response>;

    public record Response(string JobId);
}
