namespace MediatR.Demo.Notifications
{
    public class MyCustomPublisher: INotificationPublisher
    {
        public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
