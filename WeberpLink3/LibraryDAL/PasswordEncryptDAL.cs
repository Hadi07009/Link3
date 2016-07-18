using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LibraryDAL
{
    public class PasswordEncryptDAL
    {
        public PasswordEncryptDAL() { }
        public string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            if (originalPassword == "")
            {
                return "";
            }
            else
                return BitConverter.ToString(encodedBytes);
        }
    }
}
