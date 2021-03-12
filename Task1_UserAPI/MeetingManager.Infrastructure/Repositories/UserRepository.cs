using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Infastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext db;

        public UserRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<User> CreateAsync(User userData)
        {
            var user = db.Users.Add(userData).Entity;
            await db.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return;
            }    
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetOneAsync(int id)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateAsync(User userData)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userData.Id);
            if (user == null)
            {
                return null;
            }      
            user.Email = userData.Email;
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.Password = userData.Password;
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
    }
}
