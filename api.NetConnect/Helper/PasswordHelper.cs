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
        public static Boolean CheckPassword(Int32 userID, String password)
        {
            var user = UserDataController.GetItem(userID);

            return sha256_hash(password) == user.Password;
        }

        public static String HashPassword(String password)
        {
            return sha256_hash(password);
        }

        public static String ChangePassword(Int32 userID, String OldPassword, String NewPassword1, String NewPassword2)
        {
            if (NewPassword1 != NewPassword2)
                throw new PasswordsNotEqualException();

            if (!CheckPassword(userID, OldPassword))
                throw new WrongPasswordException();

            return HashPassword(NewPassword1);
        }

        private static String sha256_hash(string value)
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