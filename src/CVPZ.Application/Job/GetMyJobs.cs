using CVPZ.Application.Common;
using CVPZ.Core;
using CVPZ.Infrastructure.Data;
using MediatR;
using OneOf;

namespace CVPZ.Application.Job;

public static class GetMyJobs
{
    public record Request() : UserRequest, IRequest<OneOf<Response, Error>>;
    public record Response(IEnumerable<DataObjects.Job> Jobs);

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
                    .Where(x => x.UserId.Equals(request.GetUserId()))
                    .Select(x => new DataObjects.Job(
                        x.Id.ToString(),
                        x.EmployerName,
                        x.Title,
                        x.Description,
                        x.StartDate,
                        x.EndDate));
            return new Response(jobResults);
        }
    }
}
