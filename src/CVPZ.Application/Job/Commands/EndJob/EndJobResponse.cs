namespace CVPZ.Application.Job.Commands.EndJob
{
    public class EndJobResponse
    {
        public EndJobResponse(string jobId)
        {
            JobId = jobId;
        }

        public string JobId { get; }
    }
}