using System;
using WindesheimAD2021AutoVerzekeringsPremie.Implementation;
using static WindesheimAD2021AutoVerzekeringsPremie.Implementation.PremiumCalculation;

namespace WindesheimAD2021AutoVerzekeringsPremie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"  _____          _____   _____ _    _ _____  ______ ");
            Console.WriteLine(@" / ____|   /\   |  __ \ / ____| |  | |  __ \|  ____|");
            Console.WriteLine(@"| |       /  \  | |__) | (___ | |  | | |__) | |__   ");
            Console.WriteLine(@"| |      / /\ \ |  _  / \___ \| |  | |  _  /|  __|  ");
            Console.WriteLine(@"| |____ / ____ \| | \ \ ____) | |__| | | \ \| |____ ");
            Console.WriteLine(@" \_____/_/    \_\_|  \_\_____/ \____/|_|  \_\______|");
            Console.WriteLine(@"~~~ Welcome to the CarSure insurance calculator! ~~~" + "\r\n\r\n");
            Console.ResetColor();
            var startInputLine = Console.CursorTop;

            PolicyHolder policyholder = GetPolicyHolderData();
            ResetConsoleState(startInputLine);

            Vehicle vehicle = GetVehicleData();
            ResetConsoleState(startInputLine);

            InsuranceCoverage coverage = GetCoverage();
            ResetConsoleState(startInputLine);


            DisplayPremium(policyholder, vehicle, coverage);
        }

        
        private static PolicyHolder GetPolicyHolderData()
        {
            Console.WriteLine("Please provide the policy holder's details...\r\n");
            Console.WriteLine("What is the policy holder's age?");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("\r\nWhen was the policy holder's driving license acquired? (dd-MM-yyyy)");
            string licenseStartDate = Console.ReadLine();

            Console.WriteLine("\r\nPlease provide the 4 digits of the policy holder's postal code (e.g. 1234)");
            int postalCode = int.Parse(Console.ReadLine());

            Console.WriteLine("\r\nPlease provide the number of No-Claim years:");
            int noClaimYears = int.Parse(Console.ReadLine());

            return new PolicyHolder(age, licenseStartDate, postalCode, noClaimYears);
        }

        private static Vehicle GetVehicleData()
        {
            Console.WriteLine("Please provide the vehicle details...");
            Console.WriteLine("What is the vehicle's construction year?");
            int constructionYr = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the vehicle's value in euro's?");
            int value = int.Parse(Console.ReadLine());

            Console.WriteLine("What is the vehicle's power in Kw");
            int powerInKw = int.Parse(Console.ReadLine());

            return new Vehicle(powerInKw, value, constructionYr);
        }


        private static InsuranceCoverage GetCoverage()
        {
            Console.WriteLine("Please provide the desired coverage:");
            Console.WriteLine("1 - WA");
            Console.WriteLine("2 - WA +");
            Console.WriteLine("3 - All Risk");
            char coverageInput = Console.ReadKey(true).KeyChar;

            switch (coverageInput)
            {
                case '1':
                    return InsuranceCoverage.WA;
                case '2':
                    return InsuranceCoverage.WA_PLUS;
                case '3':
                    return InsuranceCoverage.ALL_RISK;               
                default:
                    DisplayError("Unknown vehicle type. Please Try again!");
                    return GetCoverage();
            }
        }

        private static void DisplayPremium(PolicyHolder policyholder, Vehicle vehicle, InsuranceCoverage coverage)
        {
            PremiumCalculation calculation = new(vehicle, policyholder, coverage);

            Console.WriteLine("Please provide the desired premium period:");
            Console.WriteLine("1 - Month");
            Console.WriteLine("2 - year");
            char periodInput = Console.ReadKey(true).KeyChar;
            
            PaymentPeriod p;
            string timeunit;

            switch (periodInput)
            {                
                case '2':
                    p = PaymentPeriod.YEAR;
                    timeunit = "year";
                    break;
                default:
                    p = PaymentPeriod.MONTH;
                    timeunit = "month";
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Your premium is {0} per {1}", calculation.PremiumPaymentAmount(p), timeunit); 
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("\r\nPress any key to quit.");
            Console.ReadKey(true);
        }

        private static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] {0}", message);
            Console.ResetColor();
        }

        public static void ResetConsoleState(int lineToClearFrom)
        {
            for (var line = Console.CursorTop; line >= lineToClearFrom; line--)
            {
                Console.SetCursorPosition(0, line);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, lineToClearFrom);
        }
    }
}
