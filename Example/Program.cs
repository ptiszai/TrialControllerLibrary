using System;
using TrialControllerLibrary;

namespace Example
{
    internal class Program
    {
        const int trialNumber = 3;
        static void Main(string[] args)
        {
            TrialClass trialClass = new TrialClass("Blabla");

            // #Example 1.
            if (trialClass.Create(trialDays: trialNumber)) {
            // current date + trial days
                Console.WriteLine("#Example 1.: Created was SUCCESS");
            }

            // #Example 2.
            /*if (trialClass.Create(trialDays: trialNumber, currentDate: false, year: 2023, months: 9, days: 1, hours: 10)) {
            // user date + trial days
                Console.WriteLine("#Example 2.: Created was SUCCESS");
            }*/

            Console.WriteLine($"Trial numbers of day {trialClass.IsTrial()}");
            //trialClass.Delete(); // delete trial mode, and commented is trialClass.Create(...) function.

            Console.WriteLine($"EXIT Escape key press!");            
            while (true)
            {
                ConsoleKeyInfo _consoleKeyInfo = Console.ReadKey();
                if (_consoleKeyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
           
            System.Environment.Exit(0);
        }
    }
}
