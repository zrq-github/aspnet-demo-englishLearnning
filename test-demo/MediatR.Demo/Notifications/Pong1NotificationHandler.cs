using System.Diagnostics;

namespace MediatR.Demo.Notifications;


public class Pong1NotificationHandler : INotificationHandler<PingNotification>
{
    public Task Handle(PingNotification notification, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Pong 1");
        return Task.CompletedTask;
    }
}