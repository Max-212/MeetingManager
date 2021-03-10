using MeetingManager.Entities;
using MeetingManager.Entities.Context;
using MeetingManager.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext db;

        public UserRepository(ApplicationContext context)
        {
            db = context;
        }

        public User CreateUser(User userData)
        {
            var user = db.Users.Add(userData).Entity;
            db.SaveChanges();
            return user;
        }

        public void DeleteUserById(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return;
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public User UpdateUser(User userData)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userData.Id);
            if (user == null) return null;
            user.Email = userData.Email;
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.Password = userData.Password;
            db.Users.Update(user);
            db.SaveChanges();
            return user;
        }
    }
}
