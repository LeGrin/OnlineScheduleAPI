using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models {
    public class Event {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<ScheduleRule> Rules { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
