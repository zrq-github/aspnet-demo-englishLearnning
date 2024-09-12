using MediatR;

namespace MediaEncoder.Domain.Events;

/// <summary>
/// 转码任务项完成事件
/// </summary>
/// <param name="Id">任务项标识</param>
/// <param name="SourceSystem">系统来源</param>
/// <param name="OutputUrl">转码成功后输出的路径</param>
public record EncodingItemCompletedEvent(Guid Id, string SourceSystem, Uri OutputUrl) : INotification;