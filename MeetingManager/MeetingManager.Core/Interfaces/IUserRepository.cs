
using MeetingManager.Core.Entities;
using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User userData);

        Task DeleteAsync(int id);

        Task<User> UpdateAsync(User userData);

        Task<Page<User>> GetPageAsync(int pageNumber, int perPage);

        Task<User> GetOneAsync(int id);

        Task<User> GetOneAsync(string email);

    }
}
