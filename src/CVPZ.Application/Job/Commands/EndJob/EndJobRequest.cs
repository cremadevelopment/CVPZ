using MediatR;

namespace CVPZ.Application.Job.Commands.EndJob
{
    public class EndJobRequest : IRequest<EndJobResponse>
    {
        public EndJobRequest(string jobId, DateTimeOffset endDate)
        {
            JobId = jobId;
            EndDate = endDate;
        }

        public string JobId { get; }
        public DateTimeOffset EndDate { get; }
    }
}
