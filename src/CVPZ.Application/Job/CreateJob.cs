using CVPZ.Application.Common;
using CVPZ.Core;
using CVPZ.Infrastructure.Data;
using MediatR;
using OneOf;
using CVPZ.Application.Common.Behaviors;
using static CVPZ.Application.Job.JobEvents;

namespace CVPZ.Application.Job;

public static class CreateJob
{
    public record Request(
        string Title,
        string EmployerName,
        string? Description,
        DateTimeOffset StartDate,
        DateTimeOffset? EndDate)
        : UserRequest, IRequest<OneOf<Response, Error>>;

    public record Response(
        string Id,
        string EmployerName,
        string Title,
        string? Description,
        DateTimeOffset StartDate,
        DateTimeOffset? EndDate);

    public class Errors
    {
        public static Error UserObjectIdInvalid => new(Code: nameof(UserObjectIdInvalid), "Must be a valid user to create a job");
        public static Error JobTitleRequired => new(Code: nameof(JobTitleRequired), "Title is required");
        public static Error JobEmployerNameRequired => new(Code: nameof(JobEmployerNameRequired), "Employer name is required");
        public static Error JobStartDateRequired => new(Code: nameof(JobStartDateRequired), "Job start date required");
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
            if (Guid.Empty == request.GetUserId())
                return Errors.UserObjectIdInvalid;

            if (string.IsNullOrWhiteSpace(request.Title))
                return Errors.JobTitleRequired;

            if (string.IsNullOrWhiteSpace(request.EmployerName))
                return Errors.JobEmployerNameRequired;

            if (DateTimeOffset.MinValue == request.StartDate || DateTimeOffset.MaxValue == request.StartDate)
                return Errors.JobStartDateRequired;

            if (request.EndDate.HasValue && request.StartDate > request.EndDate.Value)
                return Errors.JobEndDateGreaterThanStartDate;

            var entity = await MapToEntity(request);
            await PersistEntity(entity);

            var response = new Response(entity.Id.ToString(),
                entity.EmployerName,
                entity.Title,
                entity.Description,
                entity.StartDate,
                entity.EndDate);

            await _mediator.Publish(new JobCreated(entity.Id.ToString()));

            return response;
        }

        private async Task<Domain.Job> MapToEntity(Request request)
        {
            return new Domain.Job
            {
                UserId = request.GetUserId(),
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
}
