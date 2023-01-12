using CVPZ.Core;
using CVPZ.Infrastructure.Data;
using MediatR;
using OneOf;
using System.Linq.Expressions;

namespace CVPZ.Application.Job;

public static class SearchJobs
{
    public record Request(string? Title, string? Employer): IRequest<OneOf<Response, Error>>;
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
                    .Where(SearchTitle(request.Title))
                    .Where(SearchEmployer(request.Employer))
                    .Select(x => new DataObjects.Job(
                        x.Id.ToString(),
                        x.EmployerName,
                        x.Title,
                        x.Description,
                        x.StartDate,
                        x.EndDate));
            return new Response(jobResults);
        }

        private Expression<Func<Domain.Job, bool>> SearchTitle(string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return x => true;

            return x => x.Title.ToLower().Contains(title.ToLower());
        }

        private Expression<Func<Domain.Job, bool>> SearchEmployer(string? employer)
        {
            if (string.IsNullOrWhiteSpace(employer))
                return x => true;

            return x => x.EmployerName.ToLower().Contains(employer.ToLower());
        }
    }
}
