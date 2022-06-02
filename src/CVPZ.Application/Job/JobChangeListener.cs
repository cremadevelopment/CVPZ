using MediatR;
using Serilog;
using static CVPZ.Application.Job.JobEvents;

namespace CVPZ.Application.Job;

public class JobChangeListener : INotificationHandler<JobCreated>, INotificationHandler<JobEnded>
{
    private readonly JobCountService _jobCountService;
    private readonly ILogger _logger;

    public JobChangeListener(JobCountService jobCountService, ILogger logger)
    {
        this._jobCountService = jobCountService;
        this._logger = logger;
    }

    public async Task Handle(JobEnded notification, CancellationToken cancellationToken)
    {
        _jobCountService.RemoveJob();
        await Task.Run(() => _logger.Information("Job Ended! Only {JobCount} remaining.", _jobCountService.JobCount));
    }

    public async Task Handle(JobCreated notification, CancellationToken cancellationToken)
    {
        _jobCountService.AddJob();
        await Task.Run(() => _logger.Information("Job Created! Now {JobCount} jobs!", _jobCountService.JobCount));
    }
}
