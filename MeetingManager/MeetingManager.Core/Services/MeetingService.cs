using AutoMapper;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Core.Services
{
    public class MeetingService : IMeetingService
    {
        private IMeetingRepository meetingRepository;

        private IMapper mapper;

        public MeetingService(IMeetingRepository meetingRepository, IMapper mapper)
        {
            this.meetingRepository = meetingRepository;
            this.mapper = mapper;
        }

        public async Task<MeetingModel> CreateAsync(MeetingModel meetingData)
        {
            var meeting = await meetingRepository.CreateAsync(mapper.Map<Meeting>(meetingData));
            return mapper.Map<MeetingModel>(meeting);
        }

        public async Task DeleteAsync(int id)
        {
            await meetingRepository.DeleteAsync(id);
        }

        public async Task<List<MeetingModel>> GetAllAsync()
        {
            var meetings = await meetingRepository.GetAllAsync();
            return mapper.Map<List<MeetingModel>>(meetings);
        }

        public async Task<MeetingModel> GetOneAsync(int id)
        {
            var meeting = await meetingRepository.GetOneAsync(id);
            return mapper.Map<MeetingModel>(meeting);
        }

        public async Task<MeetingModel> RemovePartitipantAsync(int meetingId, int userId)
        {
            var meeting = await meetingRepository.RemovePartitipantAsync(meetingId, userId);
            return mapper.Map<MeetingModel>(meeting);
        }
        public async Task<MeetingModel> AddPartitipantAsync(int meetingId, int userId)
        {
            var meeting = await meetingRepository.AddPartitipantAsync(meetingId, userId);
            return mapper.Map<MeetingModel>(meeting);
        }

        public async Task<MeetingModel> UpdateAsync(MeetingModel meetingData)
        {
            var meeting = await meetingRepository.UpdateAsync(mapper.Map<Meeting>(meetingData));
            return mapper.Map<MeetingModel>(meeting);
        }
    }
}
