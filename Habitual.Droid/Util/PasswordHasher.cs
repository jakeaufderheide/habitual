using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;

namespace Habitual.Droid.Util
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] hashedBytes = algorithm.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedBytes);
        }
        
    }
}