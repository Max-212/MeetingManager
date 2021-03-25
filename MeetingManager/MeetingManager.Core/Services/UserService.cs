                                                                                                                                            using AutoMapper;
using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var user = await userRepository.CreateAsync(mapper.Map<User>(userData));
            return mapper.Map<UserModel>(user);
        }

        public async Task<Page<UserModel>> GetPageAsync(int pageNumber, int perPage)
        {
            var users = await userRepository.GetPageAsync(pageNumber, perPage);
            return mapper.Map<Page<UserModel>>(users);
        }
        
        public async Task<UserModel> UpdateAsync(UserModel userData)
        {
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

        public async Task<UserModel> GetOneAsync(string email)
        {
            var user = await userRepository.GetOneAsync(email);
            return mapper.Map<UserModel>(user);
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 