using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DataProvider.Context;
using OnlineSchedule.Models;

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
        public async Task<IHttpActionResult> Get()
        {
            return Ok(_context.Teachers.AsQueryable().Select(TeacherModel.MapTeacherModel));
        }

        // GET api/teacher/5
        [ResponseType(typeof(TeacherModel))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
                return BadRequest("No teacher with such id found");

            return Ok(TeacherModel.FromTeacher(teacher));
        }

        // POST api/teacher
        public void Post([FromBody]string value)
        {
        }

        // PUT api/teacher/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/teacher/5
        public void Delete(int id)
        {
        }
    }
}
