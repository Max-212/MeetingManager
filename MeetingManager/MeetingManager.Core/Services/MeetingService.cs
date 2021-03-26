using AutoMapper;
using MeetingManager.Core.Cache;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MeetingManager.Core.Services
{
    public class MeetingService : IMeetingService
    {
        private IMeetingRepository meetingRepository;

        private IMapper mapper;

        private MeetingCache meetingCache;

        public MeetingService(IMeetingRepository meetingRepository, IMapper mapper, MeetingCache meetingCache)
        {
            this.meetingRepository = meetingRepository;
            this.mapper = mapper;
            this.meetingCache = meetingCache;
        }

        public async Task<MeetingModel> CreateAsync(MeetingRequestModel meetingData)
        {
            var meeting = await meetingRepository.CreateAsync(mapper.Map<Meeting>(meetingData));
            await meetingCache.InsertMeeting(meeting);
            return mapper.Map<MeetingModel>(meeting);
        }

        public async Task DeleteAsync(int id)
        {
            await meetingRepository.DeleteAsync(id);
            await meetingCache.DeleteMeeting(id);
        }

        public async Task<List<MeetingModel>> GetAllAsync()
        {
            var meetings = await meetingCache.GetMeetings();
            return mapper.Map<List<MeetingModel>>(meetings);
        }

        public async Task<MeetingModel> GetOneAsync(int id)
        {
            var meetings = await meetingCache.GetMeetings();
            var meeting = meetings.FirstOrDefault(m => m.Id == id);
            return mapper.Map<MeetingModel>(meeting);
        }

        public async Task<MeetingModel> UpdateAsync(MeetingRequestModel meetingData)
        {
            var meeting = await meetingRepository.UpdateAsync(mapper.Map<Meeting>(meetingData));
            await meetingCache.UpdateMeeting(meeting);
            return mapper.Map<MeetingModel>(meeting);
        }
    }
}
