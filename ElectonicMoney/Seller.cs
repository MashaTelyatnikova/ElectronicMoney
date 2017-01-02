using System;
using System.Collections.Generic;
using System.Numerics;

namespace ElectonicMoney
{
    public enum Side
    {
        Left = 0,
        Right
    }

    public class IdentificationPart
    {
        public Side Side { get; set; }
        public BigInteger Part { get; set; }
    }

    public class CheckInfo
    {
        public IdentificationPart[] Identifications { get; set; }
        public string Id { get; set; }
        public BigInteger Content { get; set; }
        public BigInteger Signature { get; set; }
    }

    public class Seller
    {
        private readonly Dictionary<string, CheckInfo> checkInfos =
            new Dictionary<string, CheckInfo>();

        public Side[] SelectIdentificationSides(int n)
        {
            var selector = new Side[n];
            var random = new Random(Guid.NewGuid().GetHashCode());
            for (var i = 0; i < n - 1; ++i)
            {
                selector[i] = random.NextDouble() >= 0.5 ? Side.Left : Side.Right;
            }

            return selector;
        }

        public void TakeCheck(string id, BigInteger content, BigInteger signature, IdentificationPart[] parts)
        {
            checkInfos.Add(id,
                new CheckInfo() {Id = id, Content = content, Signature = signature, Identifications = parts});
        }


        public void CashOutChecks()
        {
            foreach (var checkInfo in checkInfos.Values)
            {
                var res = Bank.Instance.CashOut(checkInfo);
                switch (res)
                {
                    case CashOutOperationResult.ALREADY_CASHED:
                    {
                        Console.WriteLine($"Check {checkInfo.Id} has already cashed");
                        var perpetrator = Bank.Instance.GetCustomer(checkInfo.Id, checkInfo.Identifications);
                        Console.WriteLine($"Perpetrator = {perpetrator}");
                        break;
                    }
                    case CashOutOperationResult.WRONG_SIGNATURE:
                    {
                        Console.WriteLine($"Check {checkInfo.Id} has wrong signature");
                        break;
                    }
                    default:
                    {
                        Console.WriteLine($"Check {checkInfo.Id} has successfuly cashed");
                        break;
                    }
                }
            }
        }
    }
}