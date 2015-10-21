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

        public string Key { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }

        public ICollection<Event> Events { get; set; }

        [ForeignKey("CreatorId")]
        public virtual IdentityUser Creator { get; set; }

        public virtual string CreatorId { get; set; }

        [ForeignKey("ParentGroup")]
        public int? ParentGroupId { get; set; }

        public virtual Group ParentGroup { get; set; }
    }
}
