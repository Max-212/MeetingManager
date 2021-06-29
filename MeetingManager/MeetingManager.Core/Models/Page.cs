using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingManager.Core.Models
{
    public class Page<T>
    {
        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PerPage { get; set; }

        public List<T> Data { get; set; }
    }
}
