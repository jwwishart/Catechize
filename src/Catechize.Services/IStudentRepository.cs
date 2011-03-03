using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IStudentRepository
    {
        void Create(Student newStudent);
        void Save(Student student);
        Student GetById(int studentID);

        IList<Guid> GetCourses(int studentID);
        IList<Guid> GetCourses(string studentUserName);
    }
}

namespace Catechize.Services.SqlServer
{
    public class StudentRepository : IStudentRepository
    {
        public void Create(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public void Save(Student student)
        {
            throw new NotImplementedException();
        }

        public Student GetById(int studentID)
        {
            throw new NotImplementedException();
        }


        public IList<Guid> GetCourses(int studentID)
        {
            throw new NotImplementedException();
        }

        public IList<Guid> GetCourses(string studentUserName)
        {
            throw new NotImplementedException();
        }
    }
}
