using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Core.Interfaces
{
    public interface IUserService
    {
        public Task<UserModel> CreateAsync(UserModel userData);

        public Task<List<UserModel>> GetAllAsync();

        public Task<UserModel> UpdateAsync(UserModel userData);

        public Task DeleteAsync(int id);

        public Task<UserModel> GetOneAsync(int id);
    }
}
