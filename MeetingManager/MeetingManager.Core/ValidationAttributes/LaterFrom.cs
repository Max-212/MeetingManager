using MeetingManager.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingManager.Core.ValidationAttributes
{
    public class LaterFrom : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var meeting = (MeetingRequestModel)validationContext.ObjectInstance;
            if (meeting.From >= meeting.Till)
            {
                return new ValidationResult("Till date must be later than From date");
            }
            return ValidationResult.Success;
        }
    }
}
