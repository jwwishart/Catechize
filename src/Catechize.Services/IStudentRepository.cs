using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Models;

namespace Catechize.Services
{
    public interface IStudentRepository
    {
        void Create(Student newStudent);
        void Save(Student student);
        Student GetById(int studentID);
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
    }
}
