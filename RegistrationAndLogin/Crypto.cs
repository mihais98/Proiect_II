using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RegistrationAndLogin
{
    public static class Crypto
    {
        public static string Hash(string pass)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(pass))
                );
        }
    }
}