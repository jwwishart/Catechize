using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Model;

namespace Catechize.Services
{
    public interface IMembershipService
    {
        // TODO: Should I have a MinimumPasswordLength property? 
        bool ValidateUser(string username, string password);

        MembershipCreateStatus CreateUser(string username, string password, string email);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool ChangeEmail(string username, string oldEmail, string newEmail);

        bool IsUsernameAvailable(string username);
        bool IsUsernameWellFormed(string username);

        /*
         * int MinimumPasswordLength {get; }
         * 
         * CreateUser()
         * ChangePassword()
         * ChangeEmail()
         * 
         */
    }

    public abstract class MembershipServiceBase : IMembershipService
    {

        /// <summary>
        /// Invalid and unsafe character list. $ to @ are Invalid characters. Everything else is unsafe.
        /// </summary>
        public const string InvalidCharacters = "$&+,/:;=?@'\"<>#%{}|\\^~[]`";

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
    }
}
