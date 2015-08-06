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
