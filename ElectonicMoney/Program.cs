using System;
using System.IO;

namespace ElectonicMoney
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You have to specify input file that contains config to program");
                Environment.Exit(0);
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Config file doesn't exist");
                Environment.Exit(0);
            }

            try
            {
                var streamReader = new StreamReader(args[0]);
                var n = int.Parse(streamReader.ReadLine());
                var sum = int.Parse(streamReader.ReadLine());
                var type = int.Parse(streamReader.ReadLine()); //0 - ordinary, 1 - wrong sum, 2 - copy check, 3 - seller lies
                var name = streamReader.ReadLine();
                var id = streamReader.ReadLine();
                var customer = new Customer(n, sum, type, name, id);
                var signResult = customer.SignChecks();
                if (signResult.Status == SignOperationStatus.BAD_ENVELOPES)
                {
                    Console.WriteLine(signResult.Message);
                    Environment.Exit(0);
                }
                var seller = new Seller();
                var sides = seller.SelectIdentificationSides(n);

                var check = customer.Take();
                var parts = check.Select(sides);
                seller.TakeCheck(check.Id, check.Content, check.Signature, parts);
                seller.CashOutChecks();
                if (type == 3)
                {
                    seller.CashOutChecks();
                    Environment.Exit(0);
                }

                if (type == 2)
                {
                    var seller2 = new Seller();
                    var sides2 = seller2.SelectIdentificationSides(n);
                    var parts2 = check.Select(sides2);
                    seller2.TakeCheck(check.Id, check.Content, check.Signature, parts2);
                    seller2.CashOutChecks();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"You have given incorrect config file {ex.Message}");
            }
        }
    }
}