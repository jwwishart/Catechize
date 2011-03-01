using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IPartRepository
    {
        void Create(Part newPart);
        void Save(Part part);
        Part GetById(int partID);
    }
}

namespace Catechize.Services.SqlServer
{
    public class PartRepository : IPartRepository
    {
        public void Create(Part newPart)
        {
            throw new NotImplementedException();
        }

        public void Save(Part part)
        {
            throw new NotImplementedException();
        }

        public Part GetById(int partID)
        {
            throw new NotImplementedException();
        }
    }
}
