namespace CVPZ.Domain;

public class Job : BaseEntity
{
    public Guid UserId { get; set; }
    public string EmployerName { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}
