using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Catechize.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Catechize.Services.SqlServer
{
    public class CatDbContext : System.Data.Entity.DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserProfile> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany<CourseRegistration>(up => up.CourseRegistrations)
                .WithOptional()
                .HasForeignKey(k => k.CourseID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UserProfile>()
                .HasMany<CourseRegistration>(up => up.CourseRegistrations)
                .WithOptional()
                .HasForeignKey(i => i.UserID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Course>()
                .HasMany<Part>(p => p.Parts)
                .WithOptional()
                .HasForeignKey(k => k.CourseID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Part>()
                .HasMany<Page>(p => p.Pages)
                .WithOptional()
                .HasForeignKey(k => k.PartID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CourseLanguage>()
                .Ignore(c => c.Culture);

            modelBuilder.Entity<PartLanguage>()
                .Ignore(p => p.Culture);

            // Don't add this as it will cause it to be added as an entity and fail!
            //modelBuilder.Entity<HasCulture>()
            //    .Ignore(t => t.Culture);

        }

    }
}
