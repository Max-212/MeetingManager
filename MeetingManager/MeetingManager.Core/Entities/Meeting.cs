using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MeetingManager.Core.Entities
{
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime From { get; set; }
        
        public DateTime Till { get; set; }

        public string Description { get; set; }

        public List<User> Partitipants { get; set; }

    }
}
