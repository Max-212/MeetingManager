using MeetingManager.Core.Interfaces;
using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Core.ValidationAttributes
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userModel = (UserModel)validationContext.ObjectInstance;
            var userService = (IUserService)validationContext.GetService(typeof(IUserService));

            if (userService.GetOneAsync(userModel.Email).Result != null)
            {
                return new ValidationResult("This Email already in use");
            }

            return ValidationResult.Success;
        }
    }
}
