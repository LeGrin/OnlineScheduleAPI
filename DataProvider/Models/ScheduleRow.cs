using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models {
    public class ScheduleRow {

        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("ScheduleRule")]
        public int ScheduleRuleId { get; set; }

        public virtual ScheduleRule ScheduleRule { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public DateTime Start { get; set; }

        public int DurationInMinutes { get; set; }

        public string Room { get; set; }

        public string Lecturer { get; set; }
    }
}
