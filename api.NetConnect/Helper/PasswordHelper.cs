using api.NetConnect.data.Entity;
using api.NetConnect.DataControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace api.NetConnect.Helper
{
    public class PasswordHelper
    {
        public static Boolean CheckPassword(User User, String password)
        {
            var user = UserDataController.GetItem(User.ID);

            return HashPassword(password, User.PasswordSalt) == user.Password;
        }

        public static String CreatePassword(String Password, out String Salt)
        {
            Salt = RandomizeSalt();

            return HashPassword(Password, Salt);
        }

        public static String HashPassword(String password, String salt)
        {
            return HashSHA256(salt + password);
        }

        public static String ChangePassword(User User, String OldPassword, String NewPassword1, String NewPassword2)
        {
            if (NewPassword1 != NewPassword2)
                throw new PasswordsNotEqualException();

            if (!CheckPassword(User, OldPassword))
                throw new WrongPasswordException();

            return HashPassword(NewPassword1, User.PasswordSalt);
        }

        private static String HashSHA256(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private static String RandomizeSalt(Int32 SaltLength = 10)
        {
            StringBuilder Sb = new StringBuilder();

            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < SaltLength; i++)
            {
                Sb.Append(rand.Next(0, 9).ToString());
            }

            return Sb.ToString();
        }

        #region Exceptions
        [Serializable]
        public class WrongPasswordException : Exception
        {
            public WrongPasswordException()
                : base("Password nicht korrekt.")
            { }

            public WrongPasswordException(string message)
                : base("Password nicht korrekt.")
            { }

            public WrongPasswordException(string message, Exception innerException)
                : base("Password nicht korrekt.", innerException)
            { }
        }

        [Serializable]
        public class PasswordsNotEqualException : Exception
        {
            public PasswordsNotEqualException()
                : base("Passwörter stimmen nicht überein.")
            { }

            public PasswordsNotEqualException(string message)
                : base("Passwörter stimmen nicht überein.")
            { }

            public PasswordsNotEqualException(string message, Exception innerException)
                : base("Passwörter stimmen nicht überein.", innerException)
            { }
        }
        #endregion
    }
}