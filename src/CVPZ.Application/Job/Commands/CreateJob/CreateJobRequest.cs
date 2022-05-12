using MediatR;

namespace CVPZ.Application.Job.Commands.CreateJob;

public class CreateJobRequest : IRequest<CreateJobResponse>
{
    public CreateJobRequest(
        string title,
        string employerName,
        string? description,
        DateTimeOffset startDate,
        DateTimeOffset? endDate)
    {
        Title = title;
        EmployerName = employerName;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Title { get; }
    public string EmployerName { get; }
    public string? Description { get; }
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset? EndDate { get; }
}
