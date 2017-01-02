using System;
using System.Linq;
using System.Numerics;

namespace ElectonicMoney
{
    public class Check
    {
        public BigInteger Content { get; }
        public BigInteger Signature { get; set; }
        public string Id { get; }
        public int Sum { get; }

        private readonly Tuple<BigInteger, BigInteger>[] ids;
        public Check(int sum, int n, string customerId)
        {
            Sum = sum;
            Id = Guid.NewGuid().ToString();
            Content = sum;
            this.ids = Enumerable.Range(0, n).Select(x => SecretHelper.Divide(customerId)).ToArray();
        }
        public IdentificationPart[] Select(Side[] sides)
        {
            var result = new IdentificationPart[sides.Length];
            for (var i = 0; i < sides.Length; ++i)
            {
                result[i] = new IdentificationPart()
                {
                    Part = sides[i] == Side.Left ? ids[i].Item1 : ids[i].Item2,
                    Side = sides[i]
                };
            }

            return result;
        }
    }
}
