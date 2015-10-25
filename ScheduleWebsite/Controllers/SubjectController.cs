using DataProvider.Context;
using DataProvider.Models;
using OnlineSchedule.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace ScheduleWebsite.Controllers
{
    public class SubjectController : ApiController
    {
        DbContext _context;

        public SubjectController()
        {
            _context = new DbContext();
        }

        // GET api/teacher
        [ResponseType(typeof(IEnumerable<SubjectModel>))]
        public async Task<IHttpActionResult> GetSubjects()
        {
            return Ok(_context.Subjects.AsQueryable().Select(SubjectModel.MapSubjectModel));
        }

        // GET api/teacher/5
        [ResponseType(typeof(SubjectModel))]
        public async Task<IHttpActionResult> GetSubjects(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
                return BadRequest("No teacher with such id found");

            return Ok(TeacherModel.FromTeacher(subject));
        }

        // POST api/teacher
        [System.Web.Http.Authorize]
        public async Task<IHttpActionResult> PostSubject(SubjectModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newSubject = new Subject()
            {
                Name = model.name,
                TeacherId = model.teacherId,
                Type = (SubjectType)model.type,

            };
            _context.Subjects.Add(newSubject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                //                if (ex.IsCausedByUniqueConstraintViolation())
                throw new ApplicationException("Subject with such Key is already exists.");
                //                else
                //                   throw;
            }

            model.id = newSubject.Id;
            return CreatedAtRoute("DefaultApi", new { id = model.id }, model);
        }

        // PUT api/teacher/5
        [System.Web.Http.Authorize]
        [ResponseType(typeof(SubjectModel))]
        public async Task<IHttpActionResult> PutSubject(int id, SubjectModel model)
        {
            if (id != model.id)
                return BadRequest("No subject with such id found");
            var subject = _context.Subjects.Where(x => x.Id == id).FirstOrDefault();
            subject.TeacherId = model.teacherId;
            subject.Name = model.Name;
            subject.Type = (SubjectType)model.type;
            _context.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = model.id }, model);
        }

        [System.Web.Http.Authorize]
        [ResponseType(typeof(SubjectModel))]
        // DELETE api/teacher/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            Subject subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return NotFound();

            subject = _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return Ok(SubjectModel.FromSubject(subject));
        }
    }
}
