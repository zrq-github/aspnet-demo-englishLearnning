namespace MediatR.Demo.RequestResponse
{
    public class Ping : IRequest<string> { }

    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("RequestResponse Pong");
        }
    }

    /// <summary>
    /// 这里不会生效
    /// </summary>
    internal class Ping2Handler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("RequestResponse Pong2");
        }
    }
}
