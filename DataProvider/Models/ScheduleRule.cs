using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models {
    public class ScheduleRule {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Interval { get; set; }

        public int DurationInMinutes { get; set; }

        public string Room { get; set; }

        public string Lecturer { get; set; }
    }
}
