using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IPartRepository : IRepository<Part>
    {
        IList<Page> GetPagesByID(int partID);
    }
}
