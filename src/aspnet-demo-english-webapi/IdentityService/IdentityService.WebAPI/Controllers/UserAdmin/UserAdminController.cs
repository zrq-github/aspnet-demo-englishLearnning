using IdentityService.Domain;
using IdentityService.Infrastructure;
using IdentityService.WebAPI.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Ron.Commons;
using Ron.EventBus;

namespace IdentityService.WebAPI.Controllers.UserAdmin;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserAdminController : ControllerBase
{
    private readonly IdUserManager _userManager;
    private readonly IIdRepository _repository;
    private readonly IEventBus _eventBus;

    public UserAdminController(IdUserManager userManager, IEventBus eventBus, IIdRepository repository)
    {
        this._userManager = userManager;
        this._eventBus = eventBus;
        this._repository = repository;
    }

    /// <summary>
    /// 找到所有用户
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<UserDTO[]> FindAllUsers()
    {
        return _userManager.Users.Select(u => UserDTO.Create(u)).ToArrayAsync();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDTO> FindById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return UserDTO.Create(user);
    }

    /// <summary>
    /// 添加管理员用户
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> AddAdminUser(AddAdminUserRequest req)
    {
        (var result, var user, var password) = await _repository
            .AddAdminUserAsync(req.UserName, req.PhoneNum);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }
        //生成的密码短信发给对方
        //可以同时或者选择性的把新增用户的密码短信/邮件/打印给用户
        //体现了领域事件对于代码“高内聚、低耦合”的追求
        var userCreatedEvent = new UserCreatedEvent(user.Id, req.UserName, password, req.PhoneNum);
        _eventBus.Publish("IdentityService.User.Created", userCreatedEvent);
        return Ok();
    }

    /// <summary>
    /// 删除管理员用户
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAdminUser(Guid id)
    {
        await _repository.RemoveUserAsync(id);
        return Ok();
    }

    /// <summary>
    /// 编辑管理员用户
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateAdminUser(Guid id, EditAdminUserRequest req)
    {
        var user = await _repository.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("用户没找到");
        }
        user.PhoneNumber = req.PhoneNum;
        await _userManager.UpdateAsync(user);
        return Ok();
    }

    /// <summary>
    /// 充值管理员密码
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("{id}")]
    public async Task<ActionResult> ResetAdminUserPassword(Guid id)
    {
        (var result, var user, var password) = await _repository.ResetPasswordAsync(id);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.SumErrors());
        }
        //生成的密码短信发给对方
        var eventData = new ResetPasswordEvent(user.Id, user.UserName, password, user.PhoneNumber);
        _eventBus.Publish("IdentityService.User.PasswordReset", eventData);
        return Ok();
    }
}