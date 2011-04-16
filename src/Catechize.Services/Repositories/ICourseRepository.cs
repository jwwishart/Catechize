using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface ICourseRepository : IRepository<Course>
    {
    }

    public class CourseRepository : ICourseRepository
    {
        public Course GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public void Create(Course value)
        {
            throw new NotImplementedException();
        }

        public void Update(Course value)
        {
            throw new NotImplementedException();
        }

        public void Delete(Course value)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Course> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
