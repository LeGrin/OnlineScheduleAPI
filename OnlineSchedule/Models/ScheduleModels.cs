using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSchedule.Models {
    public class Subject {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LectureName { get; set; }

        public string RoomName { get; set; }

        public DateTime Date { get; set; }

        // only for db  purposes
        public string GroupId { get; set; }
    }

    public class Group {
        // key as a string, it will be also an id in db
        public string Id { get; set; }

        public string Name { get; set; }

        public string FacultyId { get; set; }

        public string groupKey { get; set; }
    }

    public class Classroom {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FacultyId { get; set; }
    }

    public class Faculty {

        public string Name { get; set; }
        
        // primary key
        public string FacultyId { get; set; }
    }
}