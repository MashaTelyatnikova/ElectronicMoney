using System.Numerics;

namespace ElectonicMoney
{
    public static class ByteArrayExtensions
    {
        public static BigInteger ToBigInteger(this byte[] array)
        {
            return new BigInteger(array);
        }
    }
}
