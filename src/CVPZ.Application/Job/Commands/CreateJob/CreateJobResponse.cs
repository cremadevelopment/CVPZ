namespace CVPZ.Application.Job.Commands.CreateJob;

public class CreateJobResponse
{
    public CreateJobResponse(
        string id,
        string title,
        string employerName,
        string? description,
        DateTimeOffset startDate,
        DateTimeOffset? endDate)
    {
        Id = id;
        Title = title;
        EmployerName = employerName;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Id { get; }
    public string EmployerName { get; }
    public string Title { get; }
    public string? Description { get; }
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset? EndDate { get; }
}
