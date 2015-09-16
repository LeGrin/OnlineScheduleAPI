namespace DataProvider.Migrations {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataProvider.Context.DbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataProvider.Context.DbContext context) {
            context.Faculties.AddOrUpdate(p => p.Id,
                new Models.Faculty { Id = 1, Name = "Кібернетика" },
                new Models.Faculty { Id = 2, Name = "МехМат" });
            context.Users.AddOrUpdate(p => p.Id,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityUser { Id = "sysAdmin", UserName = "sysAdmin" });
            //context.Groups.AddOrUpdate(
            //    p => p.Id, new Models.Group { Name = "MSS3", FacultyId = 1, });
        }
    }
}
