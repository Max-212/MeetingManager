
using MeetingManager.Core.Entities;
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

        Task<List<User>> GetAllAsync();

        Task<User> GetOneAsync(int id);

        Task<User> GetOneAsync(string email);
    }
}
