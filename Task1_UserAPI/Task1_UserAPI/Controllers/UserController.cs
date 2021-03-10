using MeetingManager.Core.Models;
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
        public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel request)
        {

            var user = await userService.CreateUser(request);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var user = await userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel request)
        {
            if (request.Id == 0) return BadRequest("Id is required parameter");
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
