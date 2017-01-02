using System;
using System.Numerics;

namespace ElectonicMoney
{
    public class Envelope
    {
        public bool Opened { get; private set; }

        private readonly Check check;

        private readonly BigInteger k;

        public Envelope(Check check)
        {
            this.check = check;

            
            while (true)
            {
                var random = new Random(Guid.NewGuid().GetHashCode());
                k = random.Next(2, (int)Bank.Instance.GetN() - 1);
                if (Gcd(k, Bank.Instance.GetN()) == 1)
                {
                    Console.WriteLine($"k = {k}");
                    break;
                }
            }
        }

        private static BigInteger Gcd(BigInteger a, BigInteger b)
        {
            return b == 0 ? a : Gcd(b, a.Mode(b));
        }

        public BigInteger GetContent()
        {
            var res = (check.Content*BigInteger.ModPow(k, Bank.Instance.GetE(), Bank.Instance.GetN())).Mode(
                Bank.Instance.GetN());

            return res;
        }

        public void Sign(BigInteger signature)
        {
            check.Signature = (signature*k.Invert(Bank.Instance.GetN())).Mode(Bank.Instance.GetN());
        }

        public Check Open()
        {
            Opened = true;
            return check;
        }
    }
}