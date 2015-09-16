using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataProvider.Models;

namespace OnlineSchedule.Models {
    public class SubjectModel {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Classroom { get; set; }

        public DateTime Date { get; set; }

        public int DurationInMinutes { get; set; }

        // only for db  purposes
        public string GroupId { get; set; }

        public int TeacherId { get; set; }

        public string Type { get; set; }

        public static SubjectModel FromSubject(Subject subject) {
            return new SubjectModel {
                Id = subject.Id,
                Name = subject.Name,
                TeacherId = subject.Teacher.Id
            };
        }
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