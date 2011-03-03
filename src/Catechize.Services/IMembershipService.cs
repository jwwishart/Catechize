using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IMembershipService
    {
        bool ValidateUser(string username, string password);
        MembershipCreateStatus CreateUser(string username, string password, string email);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool ChangeEmail(string username, string oldEmail, string newEmail);

        bool IsUsernameAvailable(string username);

        bool HasRole(string username, Role role);
        bool HasRole(string username, string roleName);
    }
}

namespace Catechize.Services.SqlServer
{
    public class MembershipService : IMembershipService 
    {
        public bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public MembershipCreateStatus CreateUser(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }


        public bool HasRole(string username, Role role)
        {
            throw new NotImplementedException();
        }

        public bool HasRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }


        public bool ChangeEmail(string username, string oldEmail, string newEmail)
        {
            throw new NotImplementedException();
        }

        public bool IsUsernameAvailable(string username)
        {
            throw new NotImplementedException();
        }
    }

}
