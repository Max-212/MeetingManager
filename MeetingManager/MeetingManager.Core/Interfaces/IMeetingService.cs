using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Core.Interfaces
{
    public interface IMeetingService
    {
        public Task<MeetingModel> CreateAsync(MeetingModel meetingData);

        public Task<List<MeetingModel>> GetAllAsync();

        public Task<MeetingModel> UpdateAsync(MeetingModel meetingData);

        public Task DeleteAsync(int id);

        public Task<MeetingModel> GetOneAsync(int id);

        Task<MeetingModel> AddPartitipantAsync(int meetingId, int userId);

        Task<MeetingModel> RemovePartitipantAsync(int meetingId, int userId);
    }
}
