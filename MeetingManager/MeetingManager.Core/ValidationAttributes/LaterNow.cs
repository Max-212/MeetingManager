using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingManager.Core.ValidationAttributes
{
    public class LaterNow : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var meeting = (MeetingRequestModel)validationContext.ObjectInstance;
            if(meeting.From < DateTime.Now)
            {
                return new ValidationResult("From Date must be later than current date");
            }
            return ValidationResult.Success;
        }
    }
}
