using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ElectonicMoney
{
    public static class SecretHelper
    {
        public static Tuple<BigInteger, BigInteger> Divide(string secret)
        {
            var initialBytes = secret.ToMD5();
            var randomBytes = Generate(initialBytes.Length);

            var m = initialBytes.ToBigInteger();
            var r = randomBytes.ToBigInteger();

            var s = m ^ r;
            return Tuple.Create(r, s);
        }

        private static byte[] Generate(int n)
        {
            var bytes = new byte[n];
            var random = new Random(Environment.TickCount);
            random.NextBytes(bytes);
            return bytes;
        }
    }
}
