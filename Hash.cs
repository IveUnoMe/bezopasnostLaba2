using System;
using System.Text;
using System.Security.Cryptography;

namespace Practica2
{
    internal static class Hash
    {
        internal static string GetStringHash(string password, string algorithm)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (HashAlgorithm hashAlg = algorithm == "MD5" ? (HashAlgorithm)MD5.Create() : SHA256.Create())
            {
                byte[] textData = Encoding.UTF8.GetBytes(password);
                byte[] hash = hashAlg.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }
    }
}
