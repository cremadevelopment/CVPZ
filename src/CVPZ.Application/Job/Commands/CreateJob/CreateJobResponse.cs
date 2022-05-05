namespace CVPZ.Application.Job.Commands.CreateJob;

public class CreateJobResponse
{
    public CreateJobResponse(string id, string title)
    {
        Id = id;
        Title = title;
    }

    public string Id { get; }
    public string Title { get; }
}
