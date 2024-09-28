using MediatR.Demo.Notifications;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MediatR.Demo.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MediatRController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Notification()
    {
        await _mediator.Publish(new Notifications.PingNotification());
        return Ok();
    }

    /// <summary>
    /// 发送事件，OneByOne
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SendRequest() 
    {
        var response = await _mediator.Send(new RequestResponse.Ping());
        Debug.WriteLine(response); // "Pong"
        return Ok();
    }

    //[HttpPost]
    //public async Task<IActionResult> NotificationCustomPublisher()
    //{
    //}
}
