using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catechize.Services
{
    public interface IRepository
    {
        object GetByID(int ID);
        void Save(object value);
        void Update(object value);
        void Delete(object value);
        void Delete(int ID);
        IQueryable GetAll();
    }

    public interface IRepository<T> where T: class
    {
        T GetByID(int ID);
        void Save(T value);
        void Update(T value);
        void Delete(T value);
        void Delete(int ID);
        IQueryable<T> GetAll();
    }
}
