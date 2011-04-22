using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;
using Catechize.Services;

namespace Catechize.Services
{
    public interface IStudentService
    {
        Student GetByID(string username);
        void Create(Student value);
        void Update(Student value);
        void Delete(Student value);
        void Delete(string username);
        IQueryable<Student> Query();
    }
}
