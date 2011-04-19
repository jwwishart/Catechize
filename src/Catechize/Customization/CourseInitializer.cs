using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Catechize.Services;
using System.Data.Entity;
using Catechize.Model;
using System.Web.Security;

namespace Catechize.Customization
{
    public class CourseInitializer : DropCreateDatabaseIfModelChanges<CatDbContext>
    {
        protected override void Seed(CatDbContext context)
        {
            var courses = new List<Course>
            {
                new Course { Title = "Basic Course",      Description = "This is a basic course",     Identifier = "basic",       IsEnabled = true },
                new Course { Title = "Advanced Course",   Description = "This is an advanced course", Identifier = "advanced",    IsEnabled = true },
                new Course { Title = "New Course",        Description = "This is a new course",       Identifier = "newone",      IsEnabled = false}                
            };

            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();

            Membership.CreateUser("master", "qwerty", "jwwishart@hotmail.com");
            Membership.CreateUser("jwwishart", "qwerty", "jwwishart@gmail.com");
            
            Roles.CreateRole("Master");
            Roles.CreateRole("Administrator");
            Roles.CreateRole("Translator");
            Roles.CreateRole("Student");
            Roles.CreateRole("Designer");
            Roles.CreateRole("Grader");
            Roles.CreateRole("Manager");

            Roles.AddUsersToRole(new string[] { "master" }, "Master");
            Roles.AddUsersToRole(new string[] { "jwwishart" }, "student");
        }
    }
}