using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using MeetingManager.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Infrastructure.Repositories
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
            if(userData.Id != 0)
            {
                userData.Id = 0;
            }
            var user = db.Users.Add(userData).Entity;
            await db.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return;
            }    
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }

        public async Task<Page<User>> GetUsersAsync(int pageNumber, int perPage)
        {
            var page = new Page<User>();
            page.Data = await db.Users.Skip((pageNumber - 1) * perPage)
                .Take(perPage).ToListAsync();
            page.PageNumber = pageNumber;
            page.TotalCount = await db.Users.CountAsync();
            page.PerPage = perPage;
            return page;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetOneAsync(int id)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetOneAsync(string email)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> UpdateAsync(User userData)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);
            if (user == null)
            {
                return null;
            }      
            user.Email = userData.Email;
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
    }
}
