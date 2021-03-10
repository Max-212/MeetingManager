using MeetingManager.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManager.Models.Request
{
    public class UpdateUserRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; }

        public User ToUser()
        {
            User user = new User()
            {
                Id = Id,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password
            };
            return user;
        }
    }
}
