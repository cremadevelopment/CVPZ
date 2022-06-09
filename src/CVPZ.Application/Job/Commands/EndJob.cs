using static CVPZ.Application.Job.JobEvents;

namespace CVPZ.Application.Job.Commands;

public static class EndJob
{

    public record Request(string JobId, DateTimeOffset EndDate) : IRequest<OneOf<Response, Error>>;

    public record Response(string JobId);

    public class Errors
    {
        public static Error JobIdNotValid => new(Code: nameof(JobIdNotValid), "Job id provided was not valid");
        public static Error JobNotFound => new(Code: nameof(JobNotFound), "Job not found for the given id");
        public static Error JobEndDateRequired => new(Code: nameof(JobEndDateRequired), "Job end date required");
        public static Error JobEndDateGreaterThanStartDate => new(Code: nameof(JobEndDateGreaterThanStartDate), "Job end date must be after the start date");
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
            var validId = int.TryParse(request.JobId, out int jobId);
            if (!validId)
                return Errors.JobIdNotValid;

            var job = await _context.Jobs.FindAsync(jobId);
            if (null == job)
                return Errors.JobNotFound;

            if (DateTimeOffset.MinValue == request.EndDate || DateTimeOffset.MaxValue == request.EndDate)
                return Errors.JobEndDateRequired;

            if (job.StartDate > request.EndDate)
                return Errors.JobEndDateGreaterThanStartDate;

            job.EndDate = request.EndDate;
            await _context.SaveChangesAsync();

            await _mediator.Publish(new JobEnded(request.JobId));

            return new Response(jobId.ToString());
        }
    }
}
