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
            var user = userService.GetOneAsync(userModel.Email).Result;

            if (user != null && user.Id != userModel.Id)
            {
                return new ValidationResult("This Email already in use");
            }

            return ValidationResult.Success;
        }
    }
}
