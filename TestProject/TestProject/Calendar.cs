using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class Calendar
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Write your date in format dd.mm.yyyy.:"); 
            var date = Console.ReadLine();

            if (DateTime.TryParse(date, out DateTime dateTime))
            {
                var calendar = new Calendar();
                calendar.PrintDate(dateTime);
            }
            else
            {
                Console.WriteLine("Wrong format");
                Calendar.PrintHelp();
            }
        }

        private void PrintDate(DateTime dateTime)
        {
            var tempDate = new DateTime(dateTime.Year, dateTime.Month, 1);
            
            for (int d = 1; d < 8; d++)
            {
                var day = Enum.GetName(typeof(DayOfWeek), d % 7);

                if (day.Equals("Saturday") || day.Equals("Sunday"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(String.Concat(day.Substring(0, 3), '\t'));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.Write(String.Concat(day.Substring(0, 3), '\t'));
                }
            }

            Console.Write('\n');

            var tabs = (tempDate.DayOfWeek.Equals(DayOfWeek.Sunday) ? 6 : (int)tempDate.DayOfWeek - 1);

            for (int i = 0; i < tabs; i++)
            {
                Console.Write('\t');
            }

            var workDays = 0; 

            while(tempDate.Month == dateTime.Month)
            {
                if (tempDate.DayOfWeek.Equals(DayOfWeek.Saturday))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(String.Concat(tempDate.Day, "\t"));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (tempDate.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(String.Concat(tempDate.Day, "\n"));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.Write(String.Concat(tempDate.Day, "\t"));
                    workDays++;
                }
                tempDate = tempDate.AddDays(1);
            }

            Console.WriteLine(String.Concat("\n", "Work days: ", workDays));
        }

        static void PrintHelp()
        {
            Console.WriteLine("Date must be written in format dd*mm*yyyy or dd*mm*yy");
            Console.WriteLine("* is /.- etc.");
        }
    }
}
