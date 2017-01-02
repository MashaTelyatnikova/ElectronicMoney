using System.Security.Cryptography;
using System.Text;

namespace ElectonicMoney
{
    public static class StringExtensions
    {
        public static byte[] ToMD5(this string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            return md5.ComputeHash(inputBytes);
        }
    }
}