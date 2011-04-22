using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;
using Catechize.Services;

namespace Catechize.Services.SqlServer
{
    public class StudentService : IStudentService
    {
        private CatDbContext Context { get; set; }

        public StudentService(CatDbContext context)
        {
            this.Context = context;
        }

        public Student GetByID(string username)
        {
            Student result = new Student();

            var profile = (UserProfile)UserProfile.Create(username, false);
            
            result.FirstName = profile.FirstName;
            result.LastName = profile.LastName;
            result.Initial = profile.Initial;
            result.DefaultCulture = profile.DefaultCulture;
            result.Country = profile.Country;

            return result;
        }

        public void Create(Student value)
        {
            throw new NotImplementedException();
        }

        public void Update(Student value)
        {
            throw new NotImplementedException();
        }

        public void Delete(Student value)
        {
            throw new NotImplementedException();
        }

        public void Delete(string username)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> Query()
        {
            throw new NotImplementedException();
        }
    }
}
