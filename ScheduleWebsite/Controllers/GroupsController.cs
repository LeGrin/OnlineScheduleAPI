using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
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

namespace ScheduleWebsite.Controllers {
    public class GroupsController : ApiController {

        DbContext _context;

        public GroupsController() {
            _context = new DbContext();
        }

        [ResponseType(typeof(IEnumerable<GroupModel>))]
        public async Task<IHttpActionResult> GetGroups() {
            return Ok(_context.Groups.ToList().Select(x => GroupModel.FromGroup(x)));
        }

        [ResponseType(typeof(GroupModel))]
        public async Task<IHttpActionResult> GetGroup(int id) {
            var dbEntry = await _context.Groups.FindAsync(id);
            if (dbEntry == null)
                return BadRequest("Group not found");

            return Ok(GroupModel.FromGroup(dbEntry));
        } 

        [Authorize]
        public async Task<IHttpActionResult> PostGroup(GroupModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newGroup = new Group() {
                Name = model.name,
                Key = model.key,
                FacultyId = model.facultyId,
                CreatorId = User.Identity.GetUserId<string>()
            };

            _context.Groups.Add(newGroup);
            try {
                await _context.SaveChangesAsync();
            }   
            catch (DbUpdateException ex) {
//                if (ex.IsCausedByUniqueConstraintViolation())
                    throw new ApplicationException("Group with such Key is already exists.");
//                else
 //                   throw;
            }

            model.id = newGroup.Id;
            return CreatedAtRoute("DefaultApi", new { id = model.id }, model);
        }
    }
}
