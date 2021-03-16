using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.API.Controllers
{
    [Route("api/meetings")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private IMeetingService meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
        }

        [HttpPost]
        public async Task<ActionResult<MeetingModel>> CreateMeeting([FromBody] MeetingRequestModel request)
        {
            var meeting = await meetingService.CreateAsync(request);
            return Ok(meeting);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MeetingModel>> GetMeeting(int id)
        {
            var meeting = await meetingService.GetOneAsync(id);
            if(meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        [HttpGet]
        public async Task<ActionResult<List<MeetingModel>>> GetAllMeetings()
        {
            var meetings = await meetingService.GetAllAsync();
            return Ok(meetings);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteMeeting(int id)
        {
            await meetingService.DeleteAsync(id);
            return Ok("Deleted");
        }

        [HttpPut]
        public async Task<ActionResult<MeetingModel>> UpdateMeeting([FromBody] MeetingRequestModel request)
        {
            if (request.Id == 0)
            {
                return BadRequest("Id is required parameter");
            }
            var meeting = await meetingService.UpdateAsync(request);
            if(meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }
    }
}
