using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Catechize.Services;
using System.Data.Entity;
using Catechize.Model;

namespace Catechize.Services.SqlServer
{
    public class CourseInitializer : DropCreateDatabaseIfModelChanges<CatDbContext>
    {
        protected override void Seed(CatDbContext context)
        {
            var courses = new List<Course>
            {
                new Course { Title = "Basic Course",      Description = "This is a basic course",     CourseID = "basic",       IsEnabled = true },
                new Course { Title = "Advanced Course",   Description = "This is an advanced course", CourseID = "advanced",    IsEnabled = true },
                new Course { Title = "New Course",        Description = "This is a new course",       CourseID = "newone",      IsEnabled = false}                
            };

            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();
        }
    }
}