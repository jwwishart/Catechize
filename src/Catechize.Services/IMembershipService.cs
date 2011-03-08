using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IMembershipService
    {
        // Properties
        //

        int MinPasswordLength { get; }
        bool EmailRequired { get; }

        // Methods
        //

        bool ValidateUser(string username, string password);

        MembershipCreateStatus CreateUser(string username, string password, string email);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        string ResetPassword(string username);
        bool ChangeEmail(string username, string oldEmail, string newEmail);

        bool IsUsernameAvailable(string username);
        bool IsUsernameWellFormed(string username);
    }

    public abstract class MembershipServiceBase : IMembershipService
    {
        // Properties
        //

        public abstract int MinPasswordLength { get; }
        public abstract bool EmailRequired { get; }


        // Methods
        //
        public abstract bool ValidateUser(string username, string password);
        public abstract MembershipCreateStatus CreateUser(string username, string password, string email);
        public abstract bool ChangePassword(string username, string oldPassword, string newPassword);
        public abstract bool ChangeEmail(string username, string oldEmail, string newEmail);

        public abstract bool IsUsernameAvailable(string username);

        public bool IsUsernameWellFormed(string username)
        {
            // Cant be null or empty
            if (String.IsNullOrEmpty(username))
                throw new ArgumentException("username cannot be null or empty", "username");

            // Can't have any whitespace characters
            foreach (char c in username)
            {
                // Ensure only alpha numeric
                if (char.IsLetterOrDigit(c) == false)
                    return false;
            }

            return true;
        }

        public string ResetPassword(string username)
        {
            throw new NotImplementedException();
        }
    }
}
