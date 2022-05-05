using MediatR;

namespace CVPZ.Application.Job.Commands.CreateJob
{
    public class CreateJobHandler : IRequestHandler<CreateJobRequest, CreateJobResponse>
    {
        public Task<CreateJobResponse> Handle(CreateJobRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
