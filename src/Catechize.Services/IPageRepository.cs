using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IPageRepository
    {
        void Create(Page newPage);
        void Save(Page page);
        Page GetById(int pageID);
    }
}

namespace Catechize.Services.SqlServer
{
    public class PageRepository : IPageRepository
    {
        public void Create(Page newPage)
        {
            throw new NotImplementedException();
        }

        public void Save(Page page)
        {
            throw new NotImplementedException();
        }

        public Page GetById(int pageID)
        {
            throw new NotImplementedException();
        }
    }
}
