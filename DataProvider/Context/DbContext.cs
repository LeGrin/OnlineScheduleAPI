using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
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

        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder
                .Entity<Group>()
                .Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_GroupKey", 1) { IsUnique = true }));

            base.OnModelCreating(modelBuilder);
        }
    }
}
