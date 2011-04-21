using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface ICourseService
    {
        Course GetByID(string courseID);
        void Create(Course value);
        void Update(Course value);
        void Delete(Course value);
        void Delete(int ID);
        IQueryable<Course> Query();
    }

}
