using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Catechize.Services;

namespace Catechize.Test
{

    class MemberService_Fake : MembershipServiceBase
    {
        private Dictionary<string, string> _credentials = new Dictionary<string, string>();

        public MemberService_Fake()
        {
            _credentials.Add("person1", "password1");
            _credentials.Add("person2", "password2");
            _credentials.Add("person3", "PassWord3");
        }

        public override bool ValidateUser(string username, string password)
        {
            CheckUsernameWellFormedness(username);

            if (password == null)
                throw new ArgumentNullException("password");
            if (password == string.Empty)
                throw new ArgumentException("password cannot be an empty string", "password");

            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("password", "password cannot be null");

            foreach (var di in _credentials)
            {
                if (di.Key.Equals(username, StringComparison.CurrentCultureIgnoreCase)
                     && di.Value == password)
                    return true;
            }

            return false;
        }

        public override MembershipCreateStatus CreateUser(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            CheckUsernameWellFormedness(username);

            if (oldPassword == null)
                throw new ArgumentNullException("oldPassword");
            if (oldPassword == string.Empty)
                throw new ArgumentException("oldPassword cannot be an empty string", "oldPassword");
            if (newPassword == null)
                throw new ArgumentNullException("newPassword");
            if (newPassword == string.Empty)
                throw new ArgumentException("newPassword cannot be an empty string", "newPassword");

            // Find username
            foreach (string key in _credentials.Keys)
            {
                if (key.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    if (_credentials[key].Equals(oldPassword, StringComparison.Ordinal)) {
                        _credentials[key] = newPassword;
                        return true;
                    }
                }
            }

            return false;
        }

        private static void CheckUsernameWellFormedness(string username)
        {
            if (username == null)
                throw new ArgumentNullException("username");
            if (username == string.Empty)
                throw new ArgumentException("username cannot be empty string", "username");
        }

        public override bool ChangeEmail(string username, string oldEmail, string newEmail)
        {
            throw new NotImplementedException();
        }

        public override bool IsUsernameAvailable(string username)
        {
            CheckUsernameWellFormedness(username);

            // Need to throw an exception if there is a space in the 
            // username.
            foreach (char c in username)
            {
                if (Char.IsWhiteSpace(c))
                    throw new ArgumentException("username cannot have any whitespace characters in it.", "username");
            }

            if (IsUsernameWellFormed(username) == false)
                return false;

            foreach (var dk in _credentials)
            {
                if (dk.Key == username)
                    return false;
            }

            return true;
        }
    }


    public class MembershipServiceBase_Tests
    {
        public static IMembershipService GetService()
        {
            return new MemberService_Fake();
        }

        [Fact]
        public void ValidateUser_ValidCredentialsPassed_TrueReturned()
        {
            IMembershipService service = GetService();
            Assert.True(service.ValidateUser("person1", "password1"));
            Assert.True(service.ValidateUser("person2", "password2"));
        }

        [Fact]
        public void ValidateUser_NullUsername_ExceptionThrown()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentNullException>(() => service.ValidateUser(null, "password1"));
        }

        [Fact]
        public void ValidateUser_EmptyUsername_ExceptionThrown()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>(() => service.ValidateUser(string.Empty, "password1"));
        }

        [Fact]
        public void ValidateUser_NullPassword_ExceptionThrown()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentNullException>(() => service.ValidateUser("username1", null));
        }

        [Fact]
        public void ValidateUser_EmptyPassword_ExceptionThrown()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>(() => service.ValidateUser("username1", String.Empty));
        }

        [Fact]
        public void ValidateUser_WrongCasedUsername_UsernameIsValid()
        {
            IMembershipService service = GetService();
            Assert.True(service.ValidateUser("PERSON1", "password1"));
        }

        [Fact]
        public void ValidateUser_WrongCasedPassword_CredentialsAreInvalid()
        {
            IMembershipService service = GetService();
            Assert.False(service.ValidateUser("person3", "PASSWORD3"));
        }




        [Fact]
        public void ChangePassword_ChangePasswordWithValidCredentials_ReturnsTrue()
        {
            IMembershipService service = GetService();
            Assert.True(service.ChangePassword("person1", "password1", "newPassword1"));
        }

        [Fact]
        public void ChangePassword_ChangePasswordWithValidCredentials_PasswordChanged()
        {
            IMembershipService service = GetService();
            service.ChangePassword("person1", "password1", "newPassword1");
            Assert.True(service.ValidateUser("person1", "newPassword1"));
        }

        [Fact]
        public void ChangePassword_ChangePasswordInvalidUsername_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.ChangePassword("RANDOM", "password1", "newPassword1"));
        }

        [Fact]
        public void ChangePassword_ChangePasswordWithInvalidPassword_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.ChangePassword("person1", "RANDOMPASSWORD", "newPassword1"));
        }

        [Fact]
        public void ChangePassword_NullUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentNullException>( () => service.ChangePassword(null, "password1", "newpassword"));
        }

        [Fact]
        public void ChangePassword_EmtpyUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>(() => service.ChangePassword(string.Empty, "password1", "newpassword"));
        }

        [Fact]
        public void ChangePassword_OldPasswordIsNull_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentNullException>(() => service.ChangePassword("username1", null, "newpassword"));
        }

        [Fact]
        public void ChangePassword_OldPasswordIEmpty_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>(() => service.ChangePassword("username1", string.Empty, "newpassword"));
        }

        [Fact]
        public void ChangePassword_NewPasswordIsNull_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentNullException>(() => service.ChangePassword("person1", "oldpassword", null));
        }

        [Fact]
        public void ChangePassword_NewPasswordIEmpty_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>(() => service.ChangePassword("person1", "oldpassword", string.Empty));
        }

    

        [Fact]
        public void IsUsernameAvailable_EmptyString_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>( () => service.IsUsernameAvailable(String.Empty));
        }

        [Fact]
        public void IsUsernameAvailable_NullString_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentNullException>(() => service.IsUsernameAvailable(null));
        }

        [Fact]
        public void IsUsernameAvailable_UnavailableUsername_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameAvailable("person1"));
        }

        [Fact]
        public void IsUsernameAvailable_AvailableUsername_ReturnsTrue()
        {
            IMembershipService service = GetService();
            Assert.True(service.IsUsernameAvailable("person2000"));
        }

        [Fact]
        public void IsUsernameAvailable_UsernameContainsSpaces_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>( () => service.IsUsernameAvailable("User    Name"));
        }

        [Fact]
        public void IsUsernameAvailable_UsernameWithNonAlphaNumericCharacters_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameAvailable(":"));
            Assert.False(service.IsUsernameAvailable("@"));
            Assert.False(service.IsUsernameAvailable("?"));
        }



        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter1_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("$"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter2_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("&"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter3_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("+"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter4_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed(","));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter5_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("/"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter6_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed(":"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter7_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed(";"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter8_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("="));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter9_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("?"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithReservedUrlCharacter10_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("@"));
        }

        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter1_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("<"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter2_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed(">"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter3_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("#"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter4_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("%"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter5_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("{"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter6_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("}"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter7_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("|"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter8_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("\\"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter9_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("^"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter10_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("~"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter11_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("["));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter12_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("]"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter13_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("`"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter14_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("'"));
        }
        [Fact]
        public void IsUsernameWellFormed_UserNameWithUnsafeUrlCharacter15_ReturnsFalse()
        {
            IMembershipService service = GetService();
            Assert.False(service.IsUsernameWellFormed("\""));
        }
    }
}
