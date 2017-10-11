using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace CSharpLibrary.Utility
{
    static class SimplePerfomanceCheck
    {
        public static double PerformTest(Action function)
        {
            var watch = new TimeCalculator();
            watch.Start();
            function();
            watch.Stop();
            return watch.GetElapsedTime().TotalMilliseconds;
        }

        public static double PerformTest<T>(Action<T> function, T param)
        {
            var watch = new TimeCalculator();
            watch.Start();
            function(param);
            watch.Stop();
            return watch.GetElapsedTime().TotalMilliseconds;
        }

        public static double PerformTest(Action function, int iterations)
        {
            var watch = new TimeCalculator();
            watch.Start();
            for (var i = 0; i < iterations; i++)
            {
                function();
            }
            watch.Stop();
            return watch.GetElapsedTime().TotalMilliseconds;
        }

        public static double PerformNormalisedTest(Action function, int iterations)
        {
            var watch = new TimeCalculator();
            double[] data = new double[4];
            for (var i = 0; i < 4; i++)
            {
                watch.Reset();
                watch.Start();
                for (var j = 0; j < iterations; j++)
                {
                    function();
                }
                watch.Stop();
                data[i] = watch.GetElapsedTime().TotalMilliseconds;
            }

            return data.CalculateMean();
        }

        private static double CalculateMean(this ICollection<double> data)
        {
            var mean = data.Average();
            double standardDeviation = 0;
            foreach (var d in data)
            {
                standardDeviation += Math.Pow((d - mean), 2);
            }

            standardDeviation = Math.Sqrt(standardDeviation / data.Count);

            return data.Where(m => Math.Abs(m - mean) <= standardDeviation).Average();
        }


        private class TimeCalculator
        {
            Stopwatch _stopWatch { get; set; }

            public TimeCalculator()
            {
                if (!Stopwatch.IsHighResolution)
                    throw new NotSupportedException("Your hardware doesn't support high resolution counter");

                long seed = Environment.TickCount;
                Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                _stopWatch = new Stopwatch();
            }

            public void Start()
            {
                _stopWatch.Start();
            }

            public void Stop()
            {
                _stopWatch.Stop();
            }

            public TimeSpan GetElapsedTime()
            {
                return _stopWatch.Elapsed;
            }

            public void Reset()
            {
                _stopWatch.Reset();
            }
        }
    }
}
