using System;
using System.Security.Cryptography;
using System.Text;

namespace Kasyno.Helpers
{
    /// <summary>
    /// klasa szyfrujaca
    /// </summary>
    public class Encryptor
    {
        /// <summary>
        /// metoda szyfruje algorytmem SHA256
        /// </summary>
        /// <param name="randomString">napis do zaszyfrowania</param>
        /// <returns>zaszyfrowany napis</returns>
        public static string Sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
