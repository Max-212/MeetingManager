using AutoMapper;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using MeetingManager.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace MeetingManager.Core.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<UserModel> CreateUser(UserModel userData)
        {
            userData.Password = BC.HashPassword(userData.Password);
            var user = await userRepository.CreateUser(mapper.Map<User>(userData));
            return mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = await userRepository.GetAllUsers();
            return GetUsersResponce(users);
        }

        public async Task<UserModel> UpdateUser(UserModel userData)
        {
            var user = await userRepository.UpdateUser(mapper.Map<User>(userData));
            if (user == null) return null;
            return mapper.Map<UserModel>(user);
        }

        public async Task DeleteUser(int id)
        {
            await userRepository.DeleteUserById(id);
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await userRepository.GetUserById(id);
            return mapper.Map<UserModel>(user);
        }

        private IEnumerable<UserModel> GetUsersResponce(IEnumerable<User> users)
        {
            var responseList = new List<UserModel>();
            foreach (var user in users)
            {
                responseList.Add(mapper.Map<UserModel>(user));
            }
            return responseList;
        }

    }
}
