using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface ICourseRepository
    {
        void Create(Course newCourse);
        void Save(Course course);
        Course GetById(int courseID);
    }
}

namespace Catechize.Services.SqlServer
{
    public class CourseRepository : ICourseRepository
    {
        public void Create(Course newCourse)
        {
            throw new NotImplementedException();
        }

        public void Save(Course course)
        {
            throw new NotImplementedException();
        }

        public Course GetById(int courseID)
        {
            throw new NotImplementedException();
        }
    }
}
