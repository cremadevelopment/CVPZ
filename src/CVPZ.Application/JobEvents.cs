using MediatR;

namespace CVPZ.Application.Job;
public static class JobEvents
{
    public record JobCreated(string JobId) : INotification;
    public record JobEnded(string JobId) : INotification;
}
