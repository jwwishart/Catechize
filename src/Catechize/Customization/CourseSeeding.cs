using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Catechize.Services;
using System.Data.Entity;
using Catechize.Model;

namespace Catechize.Customization
{
    public class CourseInitializer : DropCreateDatabaseIfModelChanges<CatDbContext>
    {
        protected override void Seed(CatDbContext context)
        {
            var courses = new List<Course>
            {
                new Course { CourseID = 1, Title = "Basic Course",      Description = "This is a basic course",     Identifier = "basic",       IsEnabled = true },
                new Course { CourseID = 2, Title = "Advanced Course",   Description = "This is an advanced course", Identifier = "advanced",    IsEnabled = true },
                new Course { CourseID = 3, Title = "New Course",        Description = "This is a new course",       Identifier = "newone",      IsEnabled = false}                
            };

            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();
        }
    }
}