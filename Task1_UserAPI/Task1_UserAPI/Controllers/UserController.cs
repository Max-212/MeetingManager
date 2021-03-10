using MeetingManager.Models.Request;
using MeetingManager.Models.Response;
using MeetingManager.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await userService.CreateUser(request);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var user = await userService.UpdateUser(request);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            userService.DeleteUser(id);
            return Ok("Deleted");
        }
    }
}
