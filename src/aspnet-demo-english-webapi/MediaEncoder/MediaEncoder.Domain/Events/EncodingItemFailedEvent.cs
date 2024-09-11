using MediatR;

namespace MediaEncoder.Domain.Events;

/// <summary>
/// 转码任务项失败事件
/// </summary>
public record EncodingItemFailedEvent(Guid Id, string SourceSystem, string ErrorMessage) : INotification;