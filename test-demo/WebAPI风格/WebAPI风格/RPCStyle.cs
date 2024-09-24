using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI风格
{
    public record User(int Id, string Name);

    [Route("api/[controller]")]
    [ApiController]
    public class RPCStyle : ControllerBase
    {
        // POST: api/User/CreateUser
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] User user)
        {
            // 执行创建用户操作
            return Ok(new { message = "User created successfully" });
        }

        // POST: api/User/DeleteUser
        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser([FromBody] int userId)
        {
            // 执行删除用户操作
            return Ok(new { message = "User deleted successfully" });
        }

        // POST: api/User/GetUserInfo
        [HttpPost("GetUserInfo")]
        public IActionResult GetUserInfo([FromBody] int userId)
        {
            // 获取用户信息
            var user = new User(userId, "张三");
            return Ok(user);
        }
    }
}
