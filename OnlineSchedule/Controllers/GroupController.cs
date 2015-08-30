using OnlineSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineSchedule.Controllers
{
    public class GroupController : ApiController
    {
        public IEnumerable<string[]> Get()
        {
            List<Group> Groups = new List<Group>()
            { {new Group() { Id = "K14", Name = "K-14",  groupKey = "cyb-k14-15", FacultyId = "cybernetics" } },
               {new Group() { Id = "M18-15", Name = "M-18", groupKey = "hist-m18-15", FacultyId = "history" } },
               {new Group() { Id = "MSS-1", Name = "МСС-1", groupKey = "cyb-mss1-15", FacultyId = "cybernetics" } },
               {new Group() { Id = "MSS-M1", Name = "МСС Маг-1", groupKey = "cyb-mssm1-15", FacultyId = "cybernetics" } }
            };
            List<string[]> result = new List<string[]>();
            foreach (var group in Groups)
            { result.Add(new string[] { group.Name, group.FacultyId, group.groupKey }); }
            return result;
        }
        public bool Post(string groupKey) {
            // checking groupKey
            return true;
        }

        public void Put(string groupKey) {
            // add key to db
        }

        public void Delete(string groupKey) {
            // remove key from db with all subjects for this group
            // may be performed only by admin
        }
    }
}
