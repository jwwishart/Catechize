using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Catechize.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Catechize.Services
{
    public class CatDbContext : System.Data.Entity.DbContext
    {
        public DbSet<Course> Courses { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<CourseLanguage>()
                .Ignore(c => c.Culture);

        }

    }
}
