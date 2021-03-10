using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Services.Abstract
{
    public interface IUserService
    {
        public Task<UserModel> CreateUser(UserModel userData);

        public Task<IEnumerable<UserModel>> GetAllUsers();

        public Task<UserModel> UpdateUser(UserModel userData);

        public Task DeleteUser(int id);

        public Task<UserModel> GetUserById(int id);
    }
}
