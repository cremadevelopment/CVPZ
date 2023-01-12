namespace CVPZ.Application.Job.DataObjects;

public record Job(string JobId, string EmployerName, string Title, string? Description, DateTimeOffset StartDate, DateTimeOffset? EndDate);
