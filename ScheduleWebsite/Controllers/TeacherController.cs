using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DataProvider.Context;
using DataProvider.Models;
using OnlineSchedule.Models;
using System.Data.Entity.Infrastructure;

namespace ScheduleWebsite.Controllers
{
    public class TeacherController : ApiController
    {
        DbContext _context;

        public TeacherController() {
            _context = new DbContext();
        }

        // GET api/teacher
        [ResponseType(typeof(IEnumerable<TeacherModel>))]
        public async Task<IHttpActionResult> GetTeachers()
        {
            return Ok(_context.Teachers.AsQueryable().Select(TeacherModel.MapTeacherModel));
        }

        // GET api/teacher/5
        [ResponseType(typeof(TeacherModel))]
        public async Task<IHttpActionResult> GetTeachers(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
                return BadRequest("No teacher with such id found");

            return Ok(TeacherModel.FromTeacher(teacher));
        }

        // POST api/teacher
        [Authorize]
        public async Task<IHttpActionResult> PostTeacher(TeacherModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newTeacher = new Teacher()
            {
                FirstName = model.name,
                MiddleName = model.middleName,
                LastName = model.surname,
            };
              _context.Teachers.Add(newTeacher);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                //                if (ex.IsCausedByUniqueConstraintViolation())
                throw new ApplicationException("Group with such Key is already exists.");
                //                else
                //                   throw;
            }

            model.id = newTeacher.Id;
            return CreatedAtRoute("DefaultApi", new { id = model.id }, model);
        }

        // PUT api/teacher/5
        [Authorize]
        [ResponseType(typeof(TeacherModel))]
        public async Task<IHttpActionResult> PutTeacher(int id, TeacherModel model)
        {
            if (id != model.id)
                return BadRequest("No teacher with such id found");
            var teacher = _context.Teachers.Where(x => x.Id == id).FirstOrDefault();
            teacher.LastName = model.surname;
            teacher.MiddleName = model.middleName;
            teacher.FirstName = model.name;
            _context.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = model.id }, model);
        }

        [Authorize]
        [ResponseType(typeof(TeacherModel))]
        // DELETE api/teacher/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            Teacher teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return NotFound();

            teacher = _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(TeacherModel.FromTeacher(teacher));
        }
    }
}
