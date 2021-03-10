
using MeetingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User userData);

        Task DeleteUserById(int id);

        Task<User> UpdateUser(User userData);

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(int id);
    }
}
