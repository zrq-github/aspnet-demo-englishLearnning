﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI风格
{
    /// <summary>
    /// Rest风格
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers([FromQuery] string role,string sort)
        {
            // 获取用户信息
            var user = new User(0, "张三");
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUsers_HttpGet()
        {
            // 获取用户信息
            var user = new User(0, "张三");
            return Ok(user);
        }

        //不允许
        //[HttpGet]
        //public IActionResult GetUsers(string role)
        //{
        //    // 获取用户信息
        //    var user = new User(0, "张三");
        //    return Ok(user);
        //}

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            // 获取用户信息
            var user = new User(id, "张三");
            return Ok(user);
        }

        //// POST: api/Users
        //[HttpPost]
        //public IActionResult CreateUser([FromBody] User user)
        //{
        //    // 创建用户
        //    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        //}

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            // 更新用户
            //user.Id = id;
            return Ok(user);
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // 删除用户
            return NoContent();
        }
    }
}
