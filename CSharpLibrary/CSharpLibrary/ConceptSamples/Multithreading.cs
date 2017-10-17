using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using CSharpLibrary.Utility;
using System.Threading;
using System.Net.Http;

namespace CSharpLibrary.ConceptSamples
{
    class Multithreading
    {
        //Task Parallel is costly. Use it if the the execution process takes a good enough time else use the sequential execution
        public static void TaskParallel()
        {
            var ra = new Random(145);
            var data = Enumerable.Range(1, 100).Select(m => ra.Next()).ToList();
            var nD = SimplePerfomanceCheck.PerformTest(() => data.Where(m => { Thread.Sleep(200); return (m / 3 + m * 5) % 6 == 0; }).ToList());
            var pD = SimplePerfomanceCheck.PerformTest(() => data.AsParallel().Where(m => { Thread.Sleep(200); return (m / 3 + m * 5) % 6 == 0; }).ToList());
            var nsD = SimplePerfomanceCheck.PerformTest(() => data.Where(m => { return (m / 3 + m * 5) % 6 == 0; }).ToList());
            var psD = SimplePerfomanceCheck.PerformTest(() => data.AsParallel().Where(m => {return (m / 3 + m * 5) % 6 == 0; }).ToList());
            Console.WriteLine(string.Format("Time taken by a simple process and long waiting process \nWith AsParallel:\t{0}\t{1} \nSequentially:\t{2}\t{3}", psD, pD, nsD, nD));
        }

        /// <summary>
        /// Testing the time taken by a process using tasks and normal execution
        /// </summary>
        /// <param name="noOfExecutions">No of times a proecess should iterate</param>
        /// <param name="processExecutionTime">Time taken by a process to complete in millisecond</param>
        public static void UsingTasks(int noOfExecutions, int processExecutionTime)
        {
            var c = SimplePerfomanceCheck.PerformTest(() =>
            {
                Task[] taskList = new Task[noOfExecutions];
                for (var i = 0; i < noOfExecutions; i++)
                {
                    taskList[i] = Task.Run(() => { Thread.Sleep(processExecutionTime); });
                }

                //continuation. Has lot more functions to handle continuation like continuewith etc
                var wt = Task.WhenAll(taskList);
                wt.Wait();
            });
            var c1 = SimplePerfomanceCheck.PerformTest(() =>
            {
                for (var i = 0; i < noOfExecutions; i++)
                {
                    Thread.Sleep(processExecutionTime);
                }
            });
            Console.WriteLine("Time taken by a {0} processes that take {1}ms to complete using Task parallel: {2}", noOfExecutions, processExecutionTime, c);
            Console.WriteLine("Time taken by a {0} processes that take {1}ms to complete using Sequential implementation: {2}", noOfExecutions, processExecutionTime, c1);
        }     
        
        public static async void test()
        {
            var x = t();
            x.Start();
            Console.WriteLine("After task call");
            var y = await x;
            Console.WriteLine("After Await");
        }
        
        public static Task<int> t()
        {
            return new Task<int>(() =>
            {
                Thread.Sleep(2000);
                return 10;
            });   
        }
    }
}
