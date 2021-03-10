using MeetingManager.Entities;
using MeetingManager.Models.Request;
using MeetingManager.Models.Response;
using MeetingManager.Repositories.Abstract;
using MeetingManager.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace MeetingManager.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<UserResponse> CreateUser(CreateUserRequest userData)
        {
            userData.Password = BC.HashPassword(userData.Password);
            var user = userRepository.CreateUser(userData.ToUser());
            return new UserResponse(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = userRepository.GetAllUsers();
            return GetUsersResponce(users);
        }

        public async Task<UserResponse> UpdateUser(UpdateUserRequest userData)
        {
            var user = userRepository.UpdateUser(userData.ToUser());
            if (user == null) return null;
            return new UserResponse(user);
        }
        public async void DeleteUser(int id)
        {
            userRepository.DeleteUserById(id);
        }

        private IEnumerable<UserResponse> GetUsersResponce(IEnumerable<User> users)
        {
            var responseList = new List<UserResponse>();
            foreach(var user in users)
            {
                responseList.Add(new UserResponse(user));
            }
            return responseList;
        }
    }
}
