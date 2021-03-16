using MeetingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Core.Interfaces
{
    public interface IMeetingRepository
    {
        Task<Meeting> CreateAsync(Meeting meetingData);

        Task DeleteAsync(int id);

        Task<Meeting> UpdateAsync(Meeting meetingData);

        Task<List<Meeting>> GetAllAsync();

        Task<Meeting> GetOneAsync(int id);

    }
}
