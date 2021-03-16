using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Core.Interfaces
{
    public interface IMeetingService
    {
        public Task<MeetingModel> CreateAsync(MeetingRequestModel meetingData);

        public Task<List<MeetingModel>> GetAllAsync();

        public Task<MeetingModel> UpdateAsync(MeetingRequestModel meetingData);

        public Task DeleteAsync(int id);

        public Task<MeetingModel> GetOneAsync(int id);

    }
}
