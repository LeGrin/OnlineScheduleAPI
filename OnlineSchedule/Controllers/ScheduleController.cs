using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineSchedule.Models;

namespace OnlineSchedule.Controllers {
    public class ScheduleController : ApiController {
        public IEnumerable<SubjectModel> Get(string groupKey, DateTime? lastEditDate, DateTime start, DateTime end) {

            if (string.IsNullOrWhiteSpace(groupKey))
                throw new InvalidOperationException("groupkey is not presented");

            // check last edit date for updates
            // if there is no updates -> return null
            // in case the server have some updated information -> return only updated information
            // if lastEditDate == null -> return full information

            var rand = new Random();
            var result = new List<SubjectModel>();
            for (int i = 1; i < 11; i++) {
                result.Add(new SubjectModel() {
                    Id = i,
                    Date = start.AddDays(rand.NextDouble() * 3),
                    Name = "Subject" + i,
                    Classroom = rand.Next(100).ToString(),
                    TeacherId = i
                });
            }

            return result;
        }
    }
}
