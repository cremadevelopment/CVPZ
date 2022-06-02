using CVPZ.Core;
using CVPZ.Infrastructure.Data;
using MediatR;
using OneOf;

namespace CVPZ.Application.Job.Queries;

public static class GetJobs
{
    public record Request(): IRequest<OneOf<Response, Error>>;
    public record Response(IEnumerable<Job> Jobs);
    public record Job(string EmployerName, string Title, string? Description, DateTimeOffset StartDate, DateTimeOffset? EndDate);

    public class Handler : IRequestHandler<Request, OneOf<Response, Error>>
    {
        private readonly CVPZContext _context;

        public Handler(CVPZContext context)
        {
            this._context = context;
        }

        public async Task<OneOf<Response, Error>> Handle(Request request, CancellationToken cancellationToken)
        {
            var jobResults = _context.Jobs
                    .Select(x => new Job(
                        x.EmployerName, x.Title, x.Description,
                        x.StartDate, x.EndDate));
            return new Response(jobResults);
        }
    }
}
