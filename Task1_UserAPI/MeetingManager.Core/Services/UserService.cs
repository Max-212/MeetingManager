using AutoMapper;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
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

        public async Task<UserModel> CreateAsync(UserModel userData)
        {
            userData.Password = BC.HashPassword(userData.Password);
            var user = await userRepository.CreateAsync(mapper.Map<User>(userData));
            return mapper.Map<UserModel>(user);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            return mapper.Map<List<UserModel>>(users);
        }
        
        public async Task<UserModel> UpdateAsync(UserModel userData)
        {
            userData.Password = BC.HashPassword(userData.Password);
            var user = await userRepository.UpdateAsync(mapper.Map<User>(userData));
            if (user == null)
            {
                return null;
            } 
            return mapper.Map<UserModel>(user);
        }

        public async Task DeleteAsync(int id)
        {
            await userRepository.DeleteAsync(id);
        }

        public async Task<UserModel> GetOneAsync(int id)
        {
            var user = await userRepository.GetOneAsync(id);
            return mapper.Map<UserModel>(user);
        }
    }
}
