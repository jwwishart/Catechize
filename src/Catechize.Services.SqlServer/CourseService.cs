using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;
using System.Data.Entity;

namespace Catechize.Services.SqlServer
{
    public class CourseService : ICourseService
    {
        private CatDbContext Context { get; set; }

        public CourseService(CatDbContext context)
        {
            this.Context = context;
        }

        public Course GetByID(string courseName)
        {
            return Context.Courses.Where(c => c.CourseID.Equals(courseName, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();
        }

        public void Create(Course value)
        {
            Context.Courses.Add(value);
            Context.SaveChanges();
        }

        public void Update(Course value)
        {
            Context.Entry(value).State = System.Data.EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(Course value)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Course> Query()
        {
            return Context.Courses.AsQueryable();
        }
    }
}
