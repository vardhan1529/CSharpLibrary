using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    class MultiprocessExecution
    {
        //Main process
        public bool MainProcess()
        {
            Console.WriteLine("Main process Thread Id: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            //starting all the process at once. These execute asynchronously. p1,p2,p3 are independent
            var p1 = Process1();
            var p2 = Process2();
            var p3 = Process3();

            return p1.Result && p2.Result && p3.Result;

        }


        // Process1.. It takes 6 secs to complete
        public async Task<bool> Process1()
        {
            Task<bool> t = new Task<bool>(() =>
            {
                Console.WriteLine("Process1 started. In the thread   " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                System.Threading.Thread.Sleep(6000);
                //Implement all the code related to you here
                return true;
            });

            t.Start();

            var result = await t;
            Console.WriteLine("Process1 Ended");

            return await t;
        }


        // Process2.. It takes 5 secs to complete 
        public async Task<bool> Process2()
        {
            Task<bool> t = new Task<bool>(() =>
            {
                Console.WriteLine("Process2 started. In the thread   " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                System.Threading.Thread.Sleep(5000);
                //Implement all the code related to you here
                return true;
            });

            t.Start();

            var result = await t;
            Console.WriteLine("Process2 Ended");

            return await t;
        }

        // Process3.. It takes 3 secs to complete
        public async Task<bool> Process3()
        {
            Task<bool> t = new Task<bool>(() =>
            {
                Console.WriteLine("Process3 started. In the thread   " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                System.Threading.Thread.Sleep(3000);
                //Implement all the code related to you here
                return true;
            });

            t.Start();

            var result = await t;
            Console.WriteLine("Process3 Ended");

            return await t;
        }
    }
}
