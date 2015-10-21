using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models {
    public class Subject {
        public int Id { get; set; }

        public string Name { get; set; }

        public SubjectType Type { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }
    }

    public enum SubjectType {
        lecture = 0,
        practik = 1,
        other = 2
    }
}
