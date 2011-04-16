using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Catechize.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;

namespace Catechize.Services
{
    public class CatDbContext : System.Data.Entity.DbContext
    {
        public DbSet<Course> Courses { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<CultureInfo>().Property(p => p.Name);
                
        }

    }
}
