using MediaEncoder.Domain.Entities;
using MediatR;

namespace MediaEncoder.Domain.Events;

/// <summary>
/// 转码任务项创建事件
/// </summary>
public record EncodingItemCreatedEvent(EncodingItem Value) : INotification;