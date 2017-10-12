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
    public class Base
    {
        public Base(int i)
        {

        }
    }

    public static class NestedClass
    {
        private class NC
        {
        }
    }

    sealed class SealedClass
    {
        public void SampleMethod()
        {

        }
    }

    public class ST
    {
        public string s { get; set; }
        public readonly static string t;
        static ST()
        {
            t = "Static field";
        }

        public ST() : this(string.Empty)
        {
        }

        public ST(string s)
        {
            this.s = s;
        }
    }

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
        }

        public static void DynamicObjectCreation()
        {
            Assembly a = Assembly.LoadFile(@"D:\My Projects\CsharpLib\CSharpLibrary\CSharpLibrary\bin\Debug\Newtonsoft.Json.dll");
            a.GetTypes().ToList().ForEach(m => { Console.WriteLine(m.FullName); });
            // Get the type to use.
            Type t = a.GetType("Newtonsoft.Json.JsonConvert");
            // Get the method to call.
            MethodInfo me = t.GetMethod("SerializeObject", new Type[] { typeof(string) });
            // Create an instance.
            //object obj = Activator.CreateInstance(t);
            var oa = new[] { new { Name = "test" } };
            //// Execute the method.
            //var xs = me.Invoke(obj, oa);

            //For static type method, there is no need of instance creation
            var xss = me.Invoke(null, oa);

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

        #region Time check
        public struct Point
        {
            public int X;
            public int Y;
            public string S;
        }

        public struct PointA
        {
            public int X;
            public int Y;
            public int Z;
            public int A;
            public string S;
        }

        public class PointC
        {
            public int X;
            public int Y;
            public string S;
        }

        public static void TestStruct()
        {
            Point p1 = new Point();
            p1.X = 5;
            p1.Y = 6;
            p1.S = "T";
            Point p2 = new Point();
            p2.X = 5;
            p2.Y = 6;
            p2.S = "S";
        }

        public static void TestClass()
        {
            PointC p1 = new PointC();
            p1.X = 5;
            p1.Y = 6;
            p1.S = "T";
            PointC p2 = new PointC();
            p2.X = 5;
            p2.Y = 6;
            p2.S = "S";
        }

        public static void TestStaticParam(Point p)
        {
            var x = p;
        }

        public static void TestClassParam(PointC p)
        {
            var x = p;
        }

        public static void TestStaticMoreParam(PointA p)
        {
            var x = p;
        }
        public static void TimeCheckForMethodExecution()
        {
            //clean Garbage
            GC.Collect();

            //wait for the finalizer queue to empty
            GC.WaitForPendingFinalizers();

            //clean Garbage
            GC.Collect();

            var t2 = SimplePerfomanceCheck.PerformTest(TestClass);
            var t1 = SimplePerfomanceCheck.PerformTest(TestStruct);


            var p3 = new PointA();
            p3.S = "SS"; p3.X = 4; p3.Y = 45; p3.Z = 456; p3.A = 789;

            //0.1504
            Stopwatch s1 = Stopwatch.StartNew();
            TestStaticMoreParam(p3);
            s1.Stop();
            var staticMoreParam = s1.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Time taken to call a method with struct parameter containing 4 int and 1 string is {staticMoreParam}");

            var p2 = new PointC() { S = "FF", X = 4, Y = 45 };
            //0.248
            Stopwatch s3 = Stopwatch.StartNew();
            TestClassParam(p2);
            s3.Stop();
            var classParam = s3.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Time taken to call a method with refernece type parameter is {classParam}");

            var p1 = new Point();
            p1.S = "SS"; p1.X = 4; p1.Y = 45;

            //0.1518
            Stopwatch s2 = Stopwatch.StartNew();
            TestStaticParam(p1);
            s2.Stop();
            var staticLessParam = s2.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Time taken to call a method with struct parameter containing 2 int and 1 string is {staticLessParam}");

            var t5 = SimplePerfomanceCheck.PerformTest(TestStaticMoreParam, p3);
            var t3 = SimplePerfomanceCheck.PerformTest(TestStaticParam, p1);
            var t4 = SimplePerfomanceCheck.PerformTest(TestClassParam, p2);
        }

        #endregion

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
