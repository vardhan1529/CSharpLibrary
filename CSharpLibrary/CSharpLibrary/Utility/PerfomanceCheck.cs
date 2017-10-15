using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    class PerfomanceCheck
    {
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
            Console.WriteLine(string.Format("Time taken to call a method with struct parameter containing 4 int and 1 string is {0}",staticMoreParam ));

            var p2 = new PointC() { S = "FF", X = 4, Y = 45 };
            //0.248
            Stopwatch s3 = Stopwatch.StartNew();
            TestClassParam(p2);
            s3.Stop();
            var classParam = s3.Elapsed.TotalMilliseconds;
            Console.WriteLine(string.Format("Time taken to call a method with refernece type parameter is {0}", classParam));

            var p1 = new Point();
            p1.S = "SS"; p1.X = 4; p1.Y = 45;

            //0.1518
            Stopwatch s2 = Stopwatch.StartNew();
            TestStaticParam(p1);
            s2.Stop();
            var staticLessParam = s2.Elapsed.TotalMilliseconds;
            Console.WriteLine("Time taken to call a method with struct parameter containing 2 int and 1 string is " + staticLessParam);

            var t5 = SimplePerfomanceCheck.PerformTest(TestStaticMoreParam, p3);
            var t3 = SimplePerfomanceCheck.PerformTest(TestStaticParam, p1);
            var t4 = SimplePerfomanceCheck.PerformTest(TestClassParam, p2);
        }
    }
}
