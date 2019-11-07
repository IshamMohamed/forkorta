using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Forkorta.Biz
{
    /// <summary>
    /// Util class for the URL shorting functionality
    /// </summary>
    public static class SubjectUrlUtil
    {
        /// <summary>
        /// Generates a short string based on the given length using RNGCryptoServiceProvider
        /// </summary>
        /// <param name="length">Length of the short string</param>
        /// <returns></returns>
        public static string GetShortUrl(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder shortString = new StringBuilder();
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                while (shortString.Length != length)
                {
                    byte[] oneByte = new byte[1];
                    provider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (valid.Contains(character))
                    {
                        shortString.Append(character);
                    }
                }
            }
            return shortString.ToString();
        }
    }
}
