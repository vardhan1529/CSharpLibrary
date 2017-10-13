using CSharpLibrary.MockRepo;
using CSharpLibrary.Modals;
using CSharpLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSharpLibrary
{
    public class Program
    {
        public ICountryRepo countryRepo;

        public Program(ICountryRepo countryRepo)
        {
            this.countryRepo = countryRepo;
        }

        static void Main(string[] args)
        {
            Debug.WriteLine("Debug Information-Product Starting ");
            #if true
            Console.WriteLine("test");
            #endif

            Console.WriteLine("From Service");
            Console.ReadKey();
        }

        public static void TestFtpDownload()
        {
            Ftp f = new Ftp("ftp://ftp.uconn.edu/48_hour/", "anonymous", "");
            f.GetResponse("Sample1");
        }

        public static void TestFtpUpload()
        {
            Ftp f = new Ftp("ftp://ftp.uconn.edu/48_hour/", "anonymous", "");
            f.UploadFile(GetAbsolutePathOfAFile("\\AppData\\SampleInfo.json"), "Sample1");
        }

        public static void TestFtpDelete()
        {
            Ftp f = new Ftp("ftp://ftp.uconn.edu/48_hour/", "anonymous", "");
            f.DeleteFile("SampleInfo.json");
        }

        public List<string> GetCountries()
        {
            var countries = countryRepo.GetCountryNames();
            for (var i = 0; i < countries.Count; i++)
            {
                countries[i] = countries[i].ToLower();
            }
            return countries;
        }

        private static string GetAbsolutePathOfAFile(string relativePath)
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + relativePath;
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
