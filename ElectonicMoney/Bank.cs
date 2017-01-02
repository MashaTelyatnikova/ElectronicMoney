using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ElectonicMoney
{
    public enum CashOutOperationResult
    {
        OK,
        ALREADY_CASHED,
        WRONG_SIGNATURE
    }

    public enum SignOperationStatus
    {
        OK,
        BAD_ENVELOPES
    }

    public class SignOperationResult
    {
        public SignOperationStatus Status { get; set; }
        public string Message { get; set; }
    }

    public class Bank
    {
        private readonly BigInteger e;
        private readonly BigInteger d;
        private readonly BigInteger n;
        private static Bank instanse;

        private static readonly Dictionary<string, IdentificationPart[]> CashedChecks =
            new Dictionary<string, IdentificationPart[]>();

        private static readonly Dictionary<string, string> customers = new Dictionary<string, string>();

        public static Bank Instance
        {
            get
            {
                if (instanse == null)
                {
                    instanse = new Bank();
                }

                return instanse;
            }
        }

        public BigInteger GetN()
        {
            return n;
        }

        public BigInteger GetE()
        {
            return e;
        }

        private Bank()
        {
            //todo возможно читать из конфига
            n = 527;
            e = 7;
            d = 343;
        }

        public void AddCustomer(string name, string iddentfication)
        {
            customers.Add(iddentfication, name);
        }

        public string GetCustomer(string id, IdentificationPart[] parts2)
        {
            var parts1 = CashedChecks[id];
            for (var i = 0; i < parts1.Length; ++i)
            {
                if (
                    (parts1[i].Side == Side.Left && parts2[i].Side == Side.Right) ||
                    (parts1[i].Side == Side.Right && parts2[i].Side == Side.Left))
                {
                    var initial = parts1[i].Part ^ parts2[i].Part;
                    var result = customers.FirstOrDefault(x => x.Key.ToMD5().ToBigInteger() == initial);
                    if (result.Value != null)
                    {
                        return result.Value + ";" + result.Key;
                    }
                }
            }
            return null;
        }

        public SignOperationResult SignEnvelopes(string name, string id, Envelope[] envelopes, int sum)
        {
            AddCustomer(name, id);
            var random = new Random(Guid.NewGuid().GetHashCode());
            var skip = random.Next(0, envelopes.Length);
            for (var i = 0; i < envelopes.Length; ++i)
            {
                if (skip != i)
                {
                    var check = envelopes[i].Open();
                    if (check.Sum != sum)
                    {
                        return new SignOperationResult()
                        {
                            Message = $"Envelope with check {check.Id} has wrong sum!",
                            Status = SignOperationStatus.BAD_ENVELOPES
                        };
                    }
                }
            }

            foreach (Envelope envelope in envelopes)
            {
                Sign(envelope);
            }

            return new SignOperationResult() {Status = SignOperationStatus.OK};
        }

        private void Sign(Envelope envelope)
        {
            envelope.Sign(
                Sign(
                    envelope.GetContent()
                )
            );
        }

        public bool CheckSignature(BigInteger signedData, BigInteger data)
        {
            var signed = Sign(data);
            return signed == signedData;
        }

        private BigInteger Sign(BigInteger data)
        {
            Console.WriteLine($"Start to sign {data}");
            var result = BigInteger.ModPow(data, d, n);
            Console.WriteLine($"Signature = {result}");
            return result;
        }

        public CashOutOperationResult CashOut(CheckInfo checkInfo)
        {
            if (CashedChecks.ContainsKey(checkInfo.Id))
            {
                return CashOutOperationResult.ALREADY_CASHED;
            }

            if (!CheckSignature(checkInfo.Signature, checkInfo.Content))
            {
                return CashOutOperationResult.WRONG_SIGNATURE;
            }

            CashedChecks.Add(checkInfo.Id, checkInfo.Identifications);

            return CashOutOperationResult.OK;
        }
    }
}