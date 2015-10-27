using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataProvider.Models;

namespace OnlineSchedule.Models {
    public class GroupModel {
        // key as a string, it will be also an id in db
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public int facultyId { get; set; }

        [Required]
        public string key { get; set; }

        public int? parentGroupId { get; set; }

        public static GroupModel FromGroup(Group group){
            return new GroupModel(){
                id = group.Id,
                name = group.Name,
                facultyId = group.FacultyId,
                key = group.Key,
                parentGroupId = group.ParentGroupId ?? 0
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

        public string middleName { get; set; }

        public string surname { get; set; }

        public static TeacherModel FromTeacher(Teacher teacher) {
            return new TeacherModel {
                id = teacher.Id,
                name = teacher.FirstName,
                middleName = teacher.MiddleName,
                surname = teacher.LastName
            };
        }

        public static Func<Teacher, TeacherModel> MapTeacherModel = (x =>
            FromTeacher(x));
    }

    public class SubjectModel {
        public int id { get; set; }

        public string name { get; set; }

        public int teacherId { get; set; }

        public int type { get; set; }

        public static SubjectModel FromSubject(Subject subject) {
            return new SubjectModel() {
                id = subject.Id,
                name = subject.Name,
                teacherId = subject.TeacherId,
                type = (int)subject.Type
            };
        }

        public static Func<Subject, SubjectModel> MapSubjectModel = (x =>
            FromSubject(x));
    }
}