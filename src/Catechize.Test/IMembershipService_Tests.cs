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
            if (IsUsernameWellFormed(username) == false)
                return false;

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
            throw new NotImplementedException();
        }

        public override bool ChangeEmail(string username, string oldEmail, string newEmail)
        {
            throw new NotImplementedException();
        }

        public override bool IsUsernameAvailable(string username)
        {
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
            Assert.Throws<ArgumentException>(() => service.ValidateUser(null, "password1"));
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
            Assert.Throws<ArgumentException>(() => service.ValidateUser("username1", null));
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
        public void IsUsernameAvailable_EmptyString_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>( () => service.IsUsernameAvailable(String.Empty));
        }

        [Fact]
        public void IsUsernameAvailable_NullString_ThrowsException()
        {
            IMembershipService service = GetService();
            Assert.Throws<ArgumentException>(() => service.IsUsernameAvailable(null));
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
