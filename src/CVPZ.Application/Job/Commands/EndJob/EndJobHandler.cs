using CVPZ.Infrastructure.Data;
using MediatR;
using Serilog;

namespace CVPZ.Application.Job.Commands.EndJob;

public class EndJobHandler : IRequestHandler<EndJobRequest, EndJobResponse>
{
    private readonly CVPZContext _context;
    private readonly ILogger _logger;

    public EndJobHandler(CVPZContext context, ILogger logger)
    {
        this._context = context;
        this._logger = logger;
    }

    public async Task<EndJobResponse> Handle(EndJobRequest request, CancellationToken cancellationToken)
    {
        var validId = int.TryParse(request.JobId, out int jobId);
        if (!validId)
        {
            _logger.Error("Job id of {jobId} was invalid.", request.JobId);
            throw new KeyNotFoundException($"Job id of {request.JobId} was invalid.");
        }

        var entity = await _context.Jobs.FindAsync(jobId);
        if (entity == null)
        {
            _logger.Error("Job with id {jobId} not found.", jobId);
            throw new KeyNotFoundException($"Job with id {jobId} not found.");
        }

        entity.EndDate = request.EndDate;
        await _context.SaveChangesAsync();

        return new EndJobResponse(jobId.ToString());
    }
}
