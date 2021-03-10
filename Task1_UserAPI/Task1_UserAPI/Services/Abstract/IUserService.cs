using MeetingManager.Models.Request;
using MeetingManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Services.Abstract
{
    public interface IUserService
    {
        public Task<UserResponse> CreateUser(CreateUserRequest userData);
        public Task<IEnumerable<UserResponse>> GetAllUsers();
        public Task<UserResponse> UpdateUser(UpdateUserRequest userData);
        public void DeleteUser(int id);
    }
}
