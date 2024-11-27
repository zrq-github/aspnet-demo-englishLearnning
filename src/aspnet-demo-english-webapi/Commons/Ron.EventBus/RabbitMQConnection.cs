using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ron.EventBus;

/// <summary>
/// RabbitMQ连接封装
/// </summary>
internal class RabbitMQConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly object sync_root = new();
    private IConnection _connection;
    private bool _disposed;

    public RabbitMQConnection(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public bool IsConnected => _connection is { IsOpen: true } && !_disposed;

    public  async Task<IChannel> CreateModelAsync()
    {
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

        var connection = await _connectionFactory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        return channel;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _connection.Dispose();
    }

    public async Task<bool> TryConnectAsync()
    {
        _connection = await _connectionFactory.CreateConnectionAsync();
        _connection.ConnectionShutdownAsync += _connection_ConnectionShutdownAsync;
        _connection.ConnectionUnblockedAsync += _connection_ConnectionUnblockedAsync;
        _connection.ConnectionBlockedAsync += _connection_ConnectionBlockedAsync;
        return true;
    }

    private Task _connection_ConnectionBlockedAsync(object sender, ConnectionBlockedEventArgs @event)
    {
        throw new NotImplementedException();
    }

    private Task _connection_ConnectionUnblockedAsync(object sender, AsyncEventArgs @event)
    {
        throw new NotImplementedException();
    }

    private async Task _connection_ConnectionShutdownAsync(object sender, ShutdownEventArgs @event)
    {
        throw new NotImplementedException();
    }
}
