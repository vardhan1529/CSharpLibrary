using CSharpLibrary.Algorithms;
using CSharpLibrary.ConceptSamples;
using CSharpLibrary.EncryptDecrypt;
using CSharpLibrary.MockRepo;
using CSharpLibrary.Modals;
using CSharpLibrary.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary
{
    public class Program
    {
        public class Product : IEquatable<Product>
        {
            public string Name { get; set; }
            public int Code { get; set; }

            public bool Equals(Product other)
            {

                //Check whether the compared object is null. 
                if (Object.ReferenceEquals(other, null)) return false;

                //Check whether the compared object references the same data. 
                if (Object.ReferenceEquals(this, other)) return true;

                //Check whether the products' properties are equal. 
                return Code.Equals(other.Code) && Name?.ToUpper() == other.Name?.ToUpper();
            }

            // If Equals() returns true for a pair of objects  
            // then GetHashCode() must return the same value for these objects. 

            public override int GetHashCode()
            {

                //Get hash code for the Name field if it is not null. 
                int hashProductName = Name == null ? 0 : Name.ToUpper().GetHashCode();

                //Get hash code for the Code field. 
                int hashProductCode = Code.GetHashCode();

                //Calculate the hash code for the product. 
                return hashProductName ^ hashProductCode;
            }
        }

        public ICountryRepo countryRepo;

        public Program(ICountryRepo countryRepo)
        {
            this.countryRepo = countryRepo;
        }

        private class StaticCheck
        {
            public List<string> Data { get; set; }
        }
        private static List<string> SD = new List<string>() { "One", "Two", "Three", "Four", "Five", "Six" };
        public static void CheckStaticRef()
        {
            var s = new StaticCheck() { Data = SD };
        }

        static async void VoidAsync()
        {
            System.Threading.Thread.Sleep(1000);
            await Task.Run(() => System.Threading.Thread.Sleep(2000));
            Console.WriteLine("Done with async void");
        }

        static void Main(string[] args)
        {
            var dic = new Dictionary<string, string>();
            dic["a"] = "Test";
            dic["b"] = "T";

            var dics = Newtonsoft.Json.JsonConvert.SerializeObject(dic,new ReadDictionaryAsArrayObjectSerializer());

            var dicd = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(dics, new ReadDictionaryAsArrayObjectSerializer());

            VoidAsync();
            CheckStaticRef();
            //To disable warnings
#pragma warning disable
            var yy = string.Join(", ", new[] { null, "asfd", null, "", "sfds" }.Where(m => !string.IsNullOrWhiteSpace(m)));
            var d1 = Convert.ToDateTime(null);
            var d2 = Convert.ToString(null);

            var x1 = string.Equals(null, "Y", StringComparison.OrdinalIgnoreCase);
            var y1 = string.Equals("Y", "U");
            var lp = new List<Product>() { new Product {  Code = 9 },
                       new Product { Name = "orange1", Code = 4 },
                       new Product { Name = "Orange1", Code = 4 },
                       new Product { Name = "lemon", Code = 12 } };

            var l1 = lp.Distinct().ToList();

            var timezones = TimeZoneInfo.GetSystemTimeZones();
            DateTime t = DateTime.UtcNow.AddMonths(1).AddDays(-2);
            var localtime = TimeZoneInfo.ConvertTimeFromUtc(t, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            var easterntime = TimeZoneInfo.ConvertTimeFromUtc(t, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            var x = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            //DynamicallyLoadObject.Load();
            //FileMetadataChange.UpdateTitle(@"D:\si.txt", "Test");
            //var f = File.ReadAllLines("D:\\input35.txt");
            ////var n = Convert.ToInt16(Console.ReadLine());
            //var res = new List<string>();
            //for (var i = 0; i < f.Length - 1; i++)
            //{
            //    //var l = Convert.ToInt16(Console.ReadLine());
            //    var l = Convert.ToInt16(f[i+1]);
            //    var str = f[i + 2].Split(' ').ToList();
            //    var pwd = f[i + 3];
            //    var output = string.Empty;
            //        res.Add(CrackPwd(str, pwd, ref output) ? output.TrimStart() : "WRONG PASSWORD");
            //    i = i + 2;
            //}

            //foreach (var r in res)
            //{
            //    Console.WriteLine(r);
            //}
            Debug.WriteLine("Debug Information-Product Starting ");
#if true

            Console.WriteLine("test");
#warning test
#endif

            Console.WriteLine("From Service");
            Console.ReadKey();
        }

        static bool CrackPwd(List<string> keyWords, string q, ref string output)
        {
            var c = 1;
            var prevflag = false;
            while (c <= q.Length)
            {
                var t = q.Substring(0, c);
                if (keyWords.Contains(t))
                {
                    if (q.Length == c)
                    {
                        q = q.Substring(c, q.Length - c);
                        c = 1;
                        output += " " + t;
                        prevflag = false;
                    }
                    else
                    {
                        c++;
                        prevflag = true;
                    }
                }
                else
                {
                    if (prevflag)
                    {
                        q = q.Substring(c - 1, q.Length - c - 1);
                        output += " " + t.Substring(0, c - 1);
                        c = 1;
                        prevflag = false;
                    }
                    else
                    {
                        c++;
                    }
                }

                if(string.IsNullOrEmpty(q))
                {
                    return true;
                }
            }

            return false;
        }
        static double superDigit(string n, int k)
        {
            var res = 0.0;
            var num = 0.0;
            foreach (var i in n)
            {
                num += Char.GetNumericValue(i);
            }
            num *= k;

            while (true)
            {
                if (num < 10)
                {
                    res = num;
                    break;
                }

                var temp = 0.0;

                foreach (var i in num.ToString())
                {
                    temp += Char.GetNumericValue(i);
                }

                num = temp;
            }

            return res;
        }

        public static void UniqueNo2(List<int> l)
        {
            var s = new Stopwatch();
            s.Start();
            l.Sort();
            var count = 0;
            for(var i=0; i< l.Count - 1; i = i+2)
            {
                if(l[i] != l[i+1])
                {

                    break;
                }
            }
            s.Stop();
            Console.WriteLine(s.Elapsed.Milliseconds);
        }

        public static void UniqueNo3(List<int> l)
        {
            var s = new Stopwatch();
            s.Start();
            var x = 0;
            for (var i = 0; i < l.Count; i++)
            {
                x ^= l[i];
            }
            s.Stop();

            Console.WriteLine(s.Elapsed.Milliseconds);
        }

        public static void SimpleFormat()
        {
            for (int val = 0; val <= 16; val++)
                Console.WriteLine("{0,3} - {1:G}", val, (ConceptSamples.ProjectLifeCycle.ProjectStatus)val);
        }

        public static async Task<int> ThreadUsageWithAwait()
        {
            var x = 0;
            Console.WriteLine("Thread managedID at the start of async method" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            System.Threading.Thread.Sleep(2000);
            Task tt = new Task(() =>
            {
                System.Threading.Thread.Sleep(2000);
                x = 34;
                Console.WriteLine("Thread managedID in the task method" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            });
            tt.Start();
            Console.WriteLine("Thread managedID befor the configure await method" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            await tt;
            Console.WriteLine("Thread managedID after the configure await method" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(x);
            return 5;
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
