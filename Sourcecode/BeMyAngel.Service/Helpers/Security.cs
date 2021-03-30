using System;
using System.Text;
using System.Security.Cryptography;

namespace BeMyAngel.Service.Helpers
{
    public static class Security
    {
        public static string GetEncryptedPassword(string Password, string EncryptKey)
        {
            return GetSha256EncryptedString(Password + EncryptKey);
        }

        public static string GetSha256EncryptedString(string value)
        {
            using (SHA256 Sha256 = SHA256.Create())
            {
                byte[] hash = Sha256.ComputeHash(Encoding.ASCII.GetBytes(value));
                string hex = "";
                foreach (byte x in hash)
                    hex += String.Format("{0:x2}", x);
                return "0x"+ hex.ToUpper();
            }
        }
    }
}
