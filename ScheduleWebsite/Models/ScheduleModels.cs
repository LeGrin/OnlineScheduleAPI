using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int GroupId { get; set; }

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

    public class GroupModel {
        // key as a string, it will be also an id in db
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public int facultyId { get; set; }

        [Required]
        public string key { get; set; }

        public static GroupModel FromGroup(Group group){
            return new GroupModel(){
                id = group.Id,
                name = group.Name,
                facultyId = group.FacultyId,
                key = group.Key
            };
        }
    }

    public class ClassroomModel {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FacultyId { get; set; }
    }

    public class FacultyModel {
        public int id { get; set; }

        public string name { get; set; }

        public static FacultyModel FromFaculty(Faculty faculty) {
            return new FacultyModel() {
                id = faculty.Id,
                name = faculty.Name
            };
        }
    }

    public class TeacherModel {
        public int id { get; set; }

        public string name { get; set; }

        public string middlename { get; set; }

        public string surname { get; set; }

        public static TeacherModel FromTeacher(Teacher teacher) {
            return new TeacherModel {
                id = teacher.Id,
                name = teacher.FirstName,
                middlename = teacher.MiddleName,
                surname = teacher.LastName
            };
        }

        public static Func<Teacher, TeacherModel> MapTeacherModel = (x =>
            FromTeacher(x));
    }
}