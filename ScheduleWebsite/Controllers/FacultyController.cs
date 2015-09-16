using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataProvider.Context;
using OnlineSchedule.Models;

namespace ScheduleWebsite.Controllers
{
    public class FacultyController : ApiController
    {
        DbContext _context;

        public FacultyController() {
            _context = new DbContext();
        }
        public async Task<IEnumerable<FacultyModel>> GetFaculties() {
            try {
                var faculties = _context.Faculties.ToList().Select(x => FacultyModel.FromFaculty(x)).ToList(); ;
                return faculties;
            }
            catch(Exception ex) {
                Trace.Write(ex.ToString());
                return null;
            }
        }
    }
}
