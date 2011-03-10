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
        bool ResetPassword(string username, out string newPassword);
        bool ChangeEmail(string username, string oldEmail, string newEmail);
        string GetEmail(string username);
        bool IsUsernameAvailable(string username);
        bool IsUsernameWellFormed(string username);
    }


    public abstract class MembershipServiceBase : IMembershipService
    {
        public MembershipServiceBase(int minPasswordLength, bool emailRequired)
        {
            if (minPasswordLength < 4)
                throw new ArgumentException("minPasswordLength must be 4 or greater", "minPasswordLength");

            this.MinPasswordLength = minPasswordLength;
            this.EmailRequired = emailRequired;
        }

        // Properties
        //

        public int MinPasswordLength { get; private set; }
        public bool EmailRequired { get; private set; }


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

        public abstract bool ResetPassword(string username, out string newPassword);

        protected string GenerateTemporaryPassword()
        {
            Random rnd = new Random(Environment.TickCount);
            StringBuilder result = new StringBuilder();
            int length = 8;

            // Generate letters in ASCII code ranging from 33-126 (alpha numeric characters
            // and some special characters, but not whitespace characters as it could 
            // cause confusion.

            while (result.Length < length)
            {
                int ch = rnd.Next(48, 122); // Zero to z in ASCII table

                // Append if a number (0-9) ASCII: 48-57
                if (ch <= 57)
                    result.Append((char)ch);

                // Append if a letter (A-Z) ASCII: 65-90
                if (ch >= 65 && ch <= 90)
                    result.Append((char)ch);

                // Append if a letter (a-z) ASCII: 97-122
                if (ch >= 97 && ch <= 122)
                    result.Append((char)ch);
            }

            return result.ToString();
        }

        public string GetEmail(string username)
        {
            throw new NotImplementedException();
        }
    }
}
