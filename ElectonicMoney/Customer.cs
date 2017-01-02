using System;
using System.Collections.Generic;
using System.Numerics;

namespace ElectonicMoney
{
    public class Customer
    {
        private readonly Stack<Envelope> envelopes = new Stack<Envelope>();
        private readonly int sum;
        private string name;
        private string id;
       
        //todo + id
        public Customer(int n, int sumPerCheck, int type, string name, string id)
        {
            sum = sumPerCheck;
            this.name = name;
            this.id = id;

            if (type == 1)
            {
                var random = new Random(Environment.TickCount);
                var index = random.Next(n);
                for (var i = 0; i < n; ++i)
                {
                    if (i != index)
                    {
                        var check = new Check(sumPerCheck, n, id);
                        Console.WriteLine($"Check {check.Id} has sum {check.Sum}");
                        envelopes.Push(new Envelope(check));
                    }
                    else
                    {
                        var check = new Check(sumPerCheck + sumPerCheck, n, id);
                        Console.WriteLine($"Check {check.Id} has sum {check.Sum}");
                        envelopes.Push(new Envelope(check));
                    }
                }
            }
            else
            {
                for (var i = 0; i < n; ++i)
                {
                    var check = new Check(sumPerCheck, n, id);
                    Console.WriteLine($"Check {check.Id} has sum {check.Sum}");
                    envelopes.Push(new Envelope(check));
                }
            }
        }

        public SignOperationResult SignChecks()
        {
            return Bank.Instance.SignEnvelopes(name, id, envelopes.ToArray(), sum);
        }

        public Check Take()
        {
            return envelopes.Pop().Open();
        }
    }
}