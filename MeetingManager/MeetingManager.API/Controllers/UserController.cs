using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
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
            var user = await userService.CreateAsync(request);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<Page<UserModel>>> GetUsers(int pageNumber, int perPage)
        {
            if(pageNumber <= 0)
            {
                return BadRequest("Wrong page number");
            }
            if(perPage <=0)
            {
                return BadRequest("perPage must be more than 0");
            }
            var page = await userService.GetUsersAsync(pageNumber, perPage);
            return Ok(page);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserModel>>> GetUsers()
        {
            var users = await userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var user = await userService.GetOneAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel request)
        {
            if (request.Id == 0)
            {
                return BadRequest("Id is required parameter");
            }                 
            var user = await userService.UpdateAsync(request);
            if (user == null)
            {
                return NotFound();
            }    
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            await userService.DeleteAsync(id);
            return Ok("Deleted");
        }
    }
}
