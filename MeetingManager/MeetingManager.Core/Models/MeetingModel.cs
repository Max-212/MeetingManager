using MeetingManager.Core.Entities;
using MeetingManager.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeetingManager.Core.Models
{
    public class MeetingModel
    {
        public int Id { get; set; }

        [Required]
        [LaterNow]
        public DateTime From { get; set; }

        [Required]
        [LaterFrom]
        public DateTime Till { get; set; }

        public string Description { get; set; }

        public List<UserModel> Participants { get; set; }
    }
}
