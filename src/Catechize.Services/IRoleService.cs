using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IRoleService
    {
        IList<Role> GetAll();
        Role GetRole(string roleName);
    }
}

namespace Catechize.Services.SqlServer
{
    public class RoleService : IRoleService
    {
        public IList<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role GetRole(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}