using System.Diagnostics;

namespace MediatR.Demo.Notifications;

public class Pong2NotificationHandler : INotificationHandler<PingNotification>
{
    public Task Handle(PingNotification notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Pong 2");
        return Task.CompletedTask;
    }
}