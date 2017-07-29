using CSharpLibrary.Modals;
using CSharpLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.ReadKey();
        }

        private static void TestCalculateWorkingDaysWithPairs()
        {
            var testData = new List<StartEndDatePair>()
            {
                new StartEndDatePair(){StartDate = DateTime.Now, EndDate=DateTime.Now.AddDays(7)},
                new StartEndDatePair(){StartDate = DateTime.Now.AddDays(4), EndDate=DateTime.Now.AddDays(10)},
            };

            var count = DateUtils.CalculateWorkingDays(testData);

            Console.WriteLine("No of working days: {0}", count);
        }
        private static void TestCalculateWorkingDays()
        {
            Console.WriteLine("Calculating the no of working days from current date to targeted date\n");
            Console.Write("Enter start date:");
            var startDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter the target date:");
            var targetDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Start Date: {0}      End Date:{1}", startDate.ToString("dd-MM-yyyy"), targetDate.ToString("dd-MM-yyyy"));
            Console.WriteLine("Total Working days: {0}", DateUtils.CalculateWorkingDaysMethod2(startDate, targetDate));
        }
    }
}
