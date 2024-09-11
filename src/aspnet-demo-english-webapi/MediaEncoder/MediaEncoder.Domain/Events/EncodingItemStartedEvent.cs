using MediatR;

namespace MediaEncoder.Domain.Events;

/// <summary>
/// 转码任务项开始事件
/// </summary>
public record EncodingItemStartedEvent(Guid Id, string SourceSystem) : INotification;