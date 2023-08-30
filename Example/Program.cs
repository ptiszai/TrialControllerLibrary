using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrialControllerLibrary;

namespace Example
{
    internal class Program
    {
        const int trialNumber = 2;
        static void Main(string[] args)
        {
            TrialClass trialClass = new TrialClass("Blabla");

            //#Example 1.
            if (trialClass.Create(trialDays: trialNumber)) {
            // current date + trial days
                Console.WriteLine("#Example 1.: Created was SUCCESS");
            }

            //#Example 2.
            /*if (trialClass.Create(trialDays: trialNumber, currentDate: false, year: 2023, months: 9, days: 1, hours: 10)) {
            // user date + trial days
                Console.WriteLine("#Example 2.: Created was SUCCESS");
            }*/

            Console.WriteLine($"Trial numbers of day {trialClass.IsTrial()}");
            //trialClass.Delete();

            Console.WriteLine($"EXIT Escape key press!");
            // while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
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
