using MeetingManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Repositories.Abstract
{
    public interface IUserRepository
    {
        User CreateUser(User userData);
        void DeleteUserById(int id);
        User UpdateUser(User userData);
        IEnumerable<User> GetAllUsers();
    }
}
