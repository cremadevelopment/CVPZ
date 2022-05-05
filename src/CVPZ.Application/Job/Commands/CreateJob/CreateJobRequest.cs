using MediatR;

namespace CVPZ.Application.Job.Commands.CreateJob;

public class CreateJobRequest : IRequest<CreateJobResponse>
{
    public CreateJobRequest(string title)
    {
        Title = title;
    }

    public string Title { get; }
}
