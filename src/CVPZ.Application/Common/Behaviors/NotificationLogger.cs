using MediatR;
using Serilog;

namespace CVPZ.Application.Common.Behaviors;

public class NotificationLogger : INotificationHandler<INotification>
{
    private readonly ILogger _logger;
    private int _notificationCount = 0;

    public NotificationLogger(ILogger logger)
    {
        this._logger = logger;
    }

    public Task Handle(INotification notification, CancellationToken cancellationToken)
    {
        _notificationCount++;
        var name = notification.GetType().Name;
        _logger.Information("CVPZ Notification: {@name} is the {@notificationCount} event.", name, _notificationCount);

        return Task.CompletedTask;
    }
}
