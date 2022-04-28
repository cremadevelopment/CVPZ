using MediatR.Pipeline;
using Serilog;

namespace CVPZ.Application.Common.Behaviors;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;

    public RequestLogger(ILogger logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        _logger.Information("CVPZ Request: {@name} {@request}", name, request);

        return Task.CompletedTask;
    }
}