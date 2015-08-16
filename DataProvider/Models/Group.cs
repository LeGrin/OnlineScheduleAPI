using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataProvider.Models {
    public class Group {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }

        public ICollection<Event> Events { get; set; }

        public virtual IdentityUser Creator { get; set; }
    }
}
