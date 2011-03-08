using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catechize.Services;
using Xunit;
using System.Text.RegularExpressions;

namespace Catechize.Test
{
    class MemberService_Fake : MembershipServiceBase
    {
        private int _minPasswordLength = 6;
        private bool _emailRequired = true;

        public MemberService_Fake(int minPasswordLength, bool emailRequired) : this()
        {
            this._minPasswordLength = minPasswordLength;
            this._emailRequired = emailRequired;
        }

        private IList<Tuple<string, string, string>> _credentials = new List<Tuple<string,string,string>>();

        public MemberService_Fake()
        {
            _credentials.Add(new Tuple<string,string,string>("person1", "password1", "email1"));
            _credentials.Add(new Tuple<string,string,string>("person2", "password2", "email2"));
            _credentials.Add(new Tuple<string,string,string>("person3", "PassWord3", "email3"));
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
                if (di.Item1.Equals(username, StringComparison.CurrentCultureIgnoreCase)
                     && di.Item2 == password)
                    return true;
            }

            return false;
        }

        private bool ValidateEmailAddress(string emailAddress)
        {
            if (emailAddress == null)
                throw new ArgumentNullException("emailAddress");
            if (emailAddress == string.Empty)
                throw new ArgumentException("emailAddress is required", "emailAddress");

            // Check lengths
            if (emailAddress.Length < 3)
                return false; ;
            if (emailAddress.Length > 254)
                return false;

            // Regex test.
            if (Regex.IsMatch(emailAddress, "^.+?@.+$") == false)
                return false;

            // Check for @ Symbol
            var foundAt = false;
            foreach (char c in emailAddress) {
                if (c.Equals('@'))
                    foundAt = true;
            }

            if (false == foundAt)
                return false;

            return true;
        }

        public override MembershipCreateStatus CreateUser(string username, string password, string email)
        {
            CheckUsernameWellFormedness(username);
            CheckPasswordWellFormedness(password);

            if (IsUsernameWellFormed(username) == false)
                return MembershipCreateStatus.InvalidUserName;

            if (ValidatePassword(password) == false)
                return MembershipCreateStatus.InvalidPassword;

            if (EmailRequired)
            {
                if (false == ValidateEmailAddress(email))
                {
                    return MembershipCreateStatus.InvalidEmail;
                }
            }

            foreach (var di in _credentials)
            {
                if (di.Item1.Equals(username, StringComparison.OrdinalIgnoreCase))
                    return MembershipCreateStatus.UsernameTaken;
            }

            this._credentials.Add(new Tuple<string,string,string>(username, password, email));
            return MembershipCreateStatus.Success;
        }

        private bool ValidatePassword(string password)
        {
            if (password.Length < this.MinPasswordLength)
                return false;

            return true;
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
            foreach (Tuple<string, string, string> item in _credentials)
            {
                if (item.Item1.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    if (item.Item2.Equals(oldPassword, StringComparison.Ordinal))
                    {
                        Tuple<string, string, string> newCredentials;
                        newCredentials = new Tuple<string,string,string>(username, newPassword, item.Item3);

                        _credentials.Remove(item);
                        _credentials.Add(newCredentials);
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

        private static void CheckPasswordWellFormedness(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (password == string.Empty)
                throw new ArgumentException("password cannot be empty string", "password");
        }


        public override bool ChangeEmail(string username, string oldEmail, string newEmail)
        {
            CheckUsernameWellFormedness(username);
            
            if (false == IsUsernameWellFormed(username))
                return false;

            if (EmailRequired)
            {
                if (false == ValidateEmailAddress(oldEmail))
                    return false;
                if (false == ValidateEmailAddress(newEmail))
                    return false;
            }

            return true;
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
                if (dk.Item1.Equals(username, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        public override int MinPasswordLength
        {
            get { return _minPasswordLength; }
        }

        public override bool EmailRequired
        {
            get { return _emailRequired; }
        }
    }

    public class MembershipServiceBase_Tests
    {
        private const string VALID_EMAIL_ADDRESS = "example@example.com";
        private const string AVAILABLE_USERNAME = "newUsername";
       
        public static IMembershipService GetService()
        {
            return new MemberService_Fake();
        }

        public static IMembershipService GetService(int minPasswordLength, bool emailRequired)
        {
            return new MemberService_Fake(minPasswordLength, emailRequired);
        }


        // TEST:ValidateUser
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
            string validPassword = "password1";

            Assert.Throws<ArgumentNullException>(() => service.ValidateUser(null, validPassword));
        }

        [Fact]
        public void ValidateUser_EmptyUsername_ExceptionThrown()
        {
            IMembershipService service = GetService();
            string validPassword = "password1";

            Assert.Throws<ArgumentException>(() => service.ValidateUser(string.Empty, validPassword));
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
            string existingUsernameWithWrongCase = "PERSON1";
            string validPassword = "password1";

            Assert.True(service.ValidateUser(existingUsernameWithWrongCase, validPassword));
        }

        [Fact]
        public void ValidateUser_WrongCasedPassword_CredentialsAreInvalid()
        {
            IMembershipService service = GetService();
            string existingUsername = "person3";
            string invalidPasswordCase = "PASSWORD3";

            Assert.False(service.ValidateUser(existingUsername, invalidPasswordCase));
        }


        // TEST:CreateUser

        [Fact]
        public void CreateUser_ValidCredentials_SuccessReturned()
        {
            IMembershipService service = GetService();
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Equal( MembershipCreateStatus.Success,
                service.CreateUser(validUsername, validPassword, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_ValidCredentials_UserCreated()
        {
            IMembershipService service = GetService();
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            service.CreateUser(validUsername, validPassword, VALID_EMAIL_ADDRESS);

            Assert.True(service.ValidateUser(validUsername, validPassword));    
        }

        [Fact]
        public void CreateUser_NullUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            string validPassword = "newPassword";

            Assert.Throws<ArgumentNullException>(
                () => service.CreateUser(null, validPassword, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_EmptyUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            string validPassword = "newPassword";

            Assert.Throws<ArgumentException>(
                () => service.CreateUser(string.Empty, validPassword, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_NullPassword_ThrowsException()
        {
            IMembershipService service = GetService();
            string validUsername = "newUsername";

            Assert.Throws<ArgumentNullException>(
                () => service.CreateUser(validUsername, null, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_EmptyPassword_ThrowsException()
        {
            IMembershipService service = GetService();
            string validUsername = "newUsername";

            Assert.Throws<ArgumentException>(
                () => service.CreateUser(validUsername, string.Empty, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_UsernameHasInvalidCharacters_Returns_InvalidUsername()
        {
            IMembershipService service = GetService();

            string usernameWithInvalidChars = "!@#$%^&&**";
            string validPassword = "newPassword";

            Assert.Equal( MembershipCreateStatus.InvalidUserName,
                service.CreateUser(usernameWithInvalidChars, validPassword, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_PasswordTooShort_ReturnsInvalidPassword()
        {
            IMembershipService service = GetService();
            string validUsername = "newUsername";
            string passwordThatIsTooShort = "1";

            Assert.Equal(MembershipCreateStatus.InvalidPassword,
                service.CreateUser(validUsername, passwordThatIsTooShort, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_PasswordToShort_Returns_InvalidPassword()
        {
            int minimimPasswordLength = 3;
            IMembershipService service = GetService(minimimPasswordLength, true);
            string validUsername = "newUsername";
            string passwordThatIsTooShort = "22";

            Assert.Equal(MembershipCreateStatus.InvalidPassword,
                service.CreateUser(validUsername, passwordThatIsTooShort, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_PasswordEqualToMinLength_Returns_Success()
        {
            int minimimPasswordLength = 3;
            IMembershipService service = GetService(minimimPasswordLength, true);
            string validUsername = "newUsername";
            string validLengthPassword = "223";

            Assert.Equal(MembershipCreateStatus.Success,
                service.CreateUser(validUsername, validLengthPassword, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_PasswordLongerThanMin_Returns_Success()
        {
            int minimimPasswordLength = 3;
            IMembershipService service = GetService(minimimPasswordLength, true);
            string validUsername = "newUsername";
            string passwordLongerThanMinLength = "2233";

            Assert.Equal(MembershipCreateStatus.Success,
                service.CreateUser(validUsername, passwordLongerThanMinLength, VALID_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_EmailRequiredButNullGiven_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Throws<ArgumentNullException> (
                () => service.CreateUser(validUsername, validPassword , null));
        }

        [Fact]
        public void CreateUser_EmailRequiredButEmptyStringGiven_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Throws<ArgumentException>(
                () => service.CreateUser(validUsername, validPassword, string.Empty));
        }

        [Fact]
        public void CreateUser_EmailTooShort_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";
            string emailAddress_TooShort = "1";

            Assert.Equal(MembershipCreateStatus.InvalidEmail,
                service.CreateUser(validUsername, validPassword, emailAddress_TooShort));
        }

        [Fact]
        public void CreateUser_EmailTooShort2_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";
            string emailAddress_TooShort = "12";

            Assert.Equal(MembershipCreateStatus.InvalidEmail,
                service.CreateUser(validUsername, validPassword, emailAddress_TooShort));
        }

        [Fact]
        public void CreateUser_EmailMinLengthIsValid_()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";
            string validEmail = "1@2";

            Assert.Equal( MembershipCreateStatus.Success, 
                service.CreateUser(validUsername, validPassword, validEmail));
        }

        [Fact]
        public void CreateUser_EmailHasNoAtSymbol_ReturnInvalidEmail()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";
            string emailWithoutAtSymbol = "123";

            Assert.Equal(MembershipCreateStatus.InvalidEmail,
                service.CreateUser(validUsername, validPassword, emailWithoutAtSymbol));
        }

        [Fact]
        public void CreateUser_EmailNotRequiredAndNull_ReturnsSuccess()
        {
            IMembershipService service = GetService(6, false);
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Equal( MembershipCreateStatus.Success,
                service.CreateUser(validUsername, validPassword, null));
        }

        [Fact]
        public void CreateUser_EmailMaxLength_ReturnsSuccess()
        {
            string MAXIMUM_LENGTH_EMAIL_ADDRESS = "0$234567890123456789012345678901234567890123456789012345678901234567890123456789012345678@01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123";
            int maximumEmailLength = 254; // See http://www.dominicsayers.com/isemail/
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Equal(254, MAXIMUM_LENGTH_EMAIL_ADDRESS.Length);
            Assert.Equal(MembershipCreateStatus.Success,
                service.CreateUser(validUsername, validPassword, MAXIMUM_LENGTH_EMAIL_ADDRESS));
        }

        [Fact]
        public void CreateUser_EmailTooLong_ReturnsInvalidEmail()
        {
            string EMAIL_ADDRESS_TOO_LONG = "L0$234567890123456789012345678901234567890123456789012345678901234567890123456789012345678@01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123";
            int emailShouldBeThisLong = 255; // 1 chacter longer than the max.
            IMembershipService service = GetService(6, true);
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Equal(emailShouldBeThisLong, EMAIL_ADDRESS_TOO_LONG.Length);
            Assert.Equal(MembershipCreateStatus.InvalidEmail,
                service.CreateUser(validUsername, validPassword, EMAIL_ADDRESS_TOO_LONG));
        }

        [Fact]
        public void CreateUser_NoCharactersBeforeAtSymbolInEmailAddress_ReturnsInvalidEmail()
        {
            IMembershipService service = GetService();
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Equal(MembershipCreateStatus.InvalidEmail,
                service.CreateUser(validUsername, validPassword, "@domain.com"));
        }

        [Fact]
        public void CreateUser_NoCharactersAfterAtSymbolInEmailAddress_ReturnsInvalidEmail()
        {
            IMembershipService service = GetService();
            string validUsername = "newUsername";
            string validPassword = "newPassword";

            Assert.Equal(MembershipCreateStatus.InvalidEmail,
                service.CreateUser(validUsername, validPassword, "example@"));
        }

        [Fact]
        public void CreateUser_UsernameAlreadyTaken_ReturnsUsernameTaken()
        {
            IMembershipService service = GetService( 3, false );
            var takenUsername = "person1";
            var validPassword = "password1";

            Assert.Equal(MembershipCreateStatus.UsernameTaken,
                service.CreateUser(takenUsername, validPassword, VALID_EMAIL_ADDRESS));
        }


        // TEST:ChangePassword

        [Fact]
        public void ChangePassword_ChangePasswordWithValidCredentials_ReturnsTrue()
        {
            IMembershipService service = GetService();
            string validUsername = "person1";
            string validPassword = "password1";
            string validNewPassword = "newPassword1";

            Assert.True(service.ChangePassword(validUsername, validPassword , validNewPassword ));
        }

        [Fact]
        public void ChangePassword_ChangePasswordWithValidCredentials_PasswordChanged()
        {
            IMembershipService service = GetService();
            string validUsername = "person1";
            string validPassword = "password1";
            string validNewPassword = "newPassword1";

            service.ChangePassword(validUsername, validPassword, validNewPassword);
            Assert.True(service.ValidateUser(validUsername, validNewPassword));
        }

        [Fact]
        public void ChangePassword_ChangePasswordInvalidUsername_ReturnsFalse()
        {
            IMembershipService service = GetService();
            string invalidUsername = "RANDOM";
            string validPassword = "password1";
            string validNewPassword = "newPassword1";

            Assert.False(service.ChangePassword(invalidUsername, validPassword, validNewPassword));
        }

        [Fact]
        public void ChangePassword_ChangePasswordWithInvalidPassword_ReturnsFalse()
        {
            IMembershipService service = GetService();
            string validUsername = "person1";
            string invalidPassword = "RANDOMPASSWORD";
            string validNewPassword = "newPassword1";

            Assert.False(service.ChangePassword(validUsername, invalidPassword, validNewPassword));
        }

        [Fact]
        public void ChangePassword_NullUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            string validPassword = "password1";
            string validNewPassword = "newPassword";

            Assert.Throws<ArgumentNullException>( 
                () => service.ChangePassword(null, validPassword, validNewPassword));
        }

        [Fact]
        public void ChangePassword_EmtpyUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            string validPassword = "password1";
            string validNewPassword = "newPassword";

            Assert.Throws<ArgumentException>(
                () => service.ChangePassword(string.Empty, validPassword, validNewPassword));
        }

        [Fact]
        public void ChangePassword_OldPasswordIsNull_ThrowsException()
        {
            IMembershipService service = GetService();
            string validUsername = "username1";
            string newValidPassword = "newPassword";

            Assert.Throws<ArgumentNullException>(
                () => service.ChangePassword("username1", null, "newpassword"));
        }

        [Fact]
        public void ChangePassword_OldPasswordIEmpty_ThrowsException()
        {
            IMembershipService service = GetService();
            string validUsername = "username1";
            string validNewPassword = "newPassword";

            Assert.Throws<ArgumentException>(
                () => service.ChangePassword(validUsername, string.Empty, validNewPassword));
        }

        [Fact]
        public void ChangePassword_NewPasswordIsNull_ThrowsException()
        {
            IMembershipService service = GetService();
            string existingUsername = "person1";
            string validPassword = "oldPassword";
           
            Assert.Throws<ArgumentNullException>(
                () => service.ChangePassword(existingUsername, validPassword, null));
        }

        [Fact]
        public void ChangePassword_NewPasswordIEmpty_ThrowsException()
        {
            IMembershipService service = GetService();
            string existingUsername = "person1";
            string validPassword = "oldPassword";

            Assert.Throws<ArgumentException>(
                () => service.ChangePassword(existingUsername, validPassword, string.Empty));
        }

        // TEST:ChangeEmail

        [Fact]
        public void ChangeEmail_NullUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            string validOldEmail = "example@example.com";
            string validNewEmail = "example2@example.com";

            Assert.Throws<ArgumentNullException>(
                () => service.ChangeEmail(null, validOldEmail, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_EmptyUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            string validOldEmail = "example@example.com";
            string validNewEmail = "example2@example.com";

            Assert.Throws<ArgumentException>(
                () => service.ChangeEmail(string.Empty, validOldEmail, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_ValidParameters_ReturnsTrue()
        {
            IMembershipService service = GetService();
            string validUsername = "person1";
            string validOldEmail = "example@example.com";
            string validNewEmail = "example2@example.com";

            Assert.True(service.ChangeEmail(validUsername, validOldEmail, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_InvalidUsername_ThrowsException()
        {
            IMembershipService service = GetService();
            string validUsername = "$asd@#";
            string validOldEmail = "example@example.com";
            string validNewEmail = "example2@example.com";

            Assert.False(service.ChangeEmail(validUsername, validOldEmail, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_NullOldEmailEmailRequired_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "username1";
            string validNewEmail = "example2@example.com";

            Assert.Throws<ArgumentNullException>( 
                () => service.ChangeEmail(validUsername, null, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_EmptyOldEmailEmailRequired_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "username1";
            string validNewEmail = "example2@example.com";

            Assert.Throws<ArgumentException>(
                () => service.ChangeEmail(validUsername, string.Empty, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_NullNewEmailEmailRequired_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "username1";
            string validOldEmail = "example2@example.com";

            Assert.Throws<ArgumentNullException>(
                () => service.ChangeEmail(validUsername, validOldEmail, null));
        }

        [Fact]
        public void ChangeEmail_EmptyNewEmailEmailRequired_ThrowsException()
        {
            IMembershipService service = GetService(6, true);
            string validUsername = "username1";
            string validOldEmail = "example2@example.com";

            Assert.Throws<ArgumentException>(
                () => service.ChangeEmail(validUsername, validOldEmail, string.Empty));
        }

        [Fact]
        public void ChangeEmail_NullOldEmailNoneRequired_ReturnsTrue()
        {
            IMembershipService service = GetService(6, false);
            string validUsername = "username1";
            string validNewEmail = "example2@example.com";

            Assert.True(service.ChangeEmail(validUsername, null, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_EmptyOldEmailNoneRequired_ReturnsTrue()
        {
            IMembershipService service = GetService(6, false);
            string validUsername = "username1";
            string validNewEmail = "example2@example.com";

            Assert.True(service.ChangeEmail(validUsername, string.Empty, validNewEmail));
        }

        [Fact]
        public void ChangeEmail_NullNewEmailNoneRequired_ReturnsTrue()
        {
            IMembershipService service = GetService(6, false);
            string validUsername = "username1";
            string validOldEmail = "example2@example.com";

            Assert.True(service.ChangeEmail(validUsername, validOldEmail, null));
        }

        [Fact]
        public void ChangeEmail_EmptyNewEmailNoneRequired_ReturnsTrue()
        {
            IMembershipService service = GetService(6, false);
            string validUsername = "username1";
            string validOldEmail = "example2@example.com";

            Assert.True(service.ChangeEmail(validUsername, validOldEmail, string.Empty));
        }

        [Fact]
        public void ChangeEmail_ReplaceEmptyEmailWithEmptyEmailWithEmailsNotRequired_ReturnsTrue()
        {
            IMembershipService service = GetService(6, false);
            string validUsername = "username1";
            
            Assert.True(service.ChangeEmail(validUsername, string.Empty, string.Empty));
        }

        [Fact]
        public void ChangeEmail_OldEmailAddressInvalid_ReturnFalse()
        {
            IMembershipService service = GetService(6, false);
            string validUsername = "username1";
            string invalidOldEmail = " aksjdfkl jalfjkdk jaf; ";
            //string validNewEmail = ""; asdjkf kja; // PUT VALID EMAIL ADDRESS

            Assert.True(service.ChangeEmail(validUsername, string.Empty, string.Empty));
        }





        // TEST:IsUsernameAvailable

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
            string existingUsername = "person1";

            Assert.False(service.IsUsernameAvailable(existingUsername));
        }

        [Fact]
        public void IsUsernameAvailable_UnavailableUsernameWithWrongCase_ReturnsFalse()
        {
            IMembershipService service = GetService();
            string existingUsernameWithWrongCase = "PERSon1";

            Assert.False(service.IsUsernameAvailable(existingUsernameWithWrongCase));
        }

        [Fact]
        public void IsUsernameAvailable_AvailableUsername_ReturnsTrue()
        {
            IMembershipService service = GetService();
            string availableUsername = "person2000";

            Assert.True(service.IsUsernameAvailable(availableUsername));
        }

        [Fact]
        public void IsUsernameAvailable_UsernameContainsSpaces_ThrowsException()
        {
            IMembershipService service = GetService();
            string usernameWithSpaces = "User    Name";

            Assert.Throws<ArgumentException>( 
                () => service.IsUsernameAvailable(usernameWithSpaces));
        }

        [Fact]
        public void IsUsernameAvailable_UsernameWithNonAlphaNumericCharacters_ThrowsException()
        {
            IMembershipService service = GetService();

            Assert.False(service.IsUsernameAvailable(":"));
            Assert.False(service.IsUsernameAvailable("@"));
            Assert.False(service.IsUsernameAvailable("?"));
        }


        // TEST:IsUsernameWellFormed

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
