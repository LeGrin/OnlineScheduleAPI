using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataProvider.Context {
    public class DbContext : IdentityDbContext<IdentityUser> {

        public DbSet<Group> Groups { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<ScheduleRule> ScheduleRules { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<ScheduleRow> ScheduleRows { get; set; } 
    }
}
