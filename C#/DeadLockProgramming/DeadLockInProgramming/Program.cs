using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeadLockInProgramming
{
    internal class Program
    {
        private static Mutex mut1 = new Mutex();
        private static Mutex mut2 = new Mutex();

        public static List<int> threadListOfCurrentResource1Holder = new List<int> { };
        public static List<int> threadListOfCurrentResource2Holder = new List<int> { };


        static void Main()
        {
            // Create the threads that will use the protected resource.

            while (true)
            {
                Thread thread1 = new Thread(new ParameterizedThreadStart(UseResource1));
                Thread thread2 = new Thread(new ParameterizedThreadStart(UseResource2));//using parameterise thread, so we can pass in a parameter.
                thread1.Start(thread1.ManagedThreadId);
                thread2.Start(thread2.ManagedThreadId);
                //Main thread wait for both thread to finish before starting a new thread to execute.
                thread2.Join(); 
                thread1.Join();
                Console.WriteLine("Both threads completely exit.");

            }

            // The main thread exits, but the application continues to
            // run until all foreground threads have exited.
        }



        // This method represents a resource that must be synchronized
        // so that only one thread at a time can enter.
        private static void UseResource1(object id)
        {
            if (threadListOfCurrentResource1Holder.Contains((int)id))
            {
                //prevent recursive loop from calling loop function.
            }
            else
            {
                
                threadListOfCurrentResource1Holder.Add((int)id);
                // Wait until it is safe to enter.
                Console.WriteLine("{0} is requesting the mutex1",
                                  id);

                mut1.WaitOne();

                Console.WriteLine("{0} has entered the protected area and gain access to mutex1",
                                  id);

                // Place code to access non-reentrant resources here.


                //Thread.Sleep(5000);//sleep thread for 5 seconds, so other thread can have time to grab other resource before this thread grab it, and to make sure deadlock occur immediately.
                UseResource2(id);
                // Simulate some work by thread sleep.
                Thread.Sleep(500);

                Console.WriteLine("{0} is leaving the protected area",
                    id);

                // Release the Mutex.
                mut1.ReleaseMutex();
                Console.WriteLine("{0} has released the mutex1",
                    id);
            }
        }
        private static void UseResource2(object id)
        {

            if (threadListOfCurrentResource2Holder.Contains((int)id))
            {
                //prevent recursive loop from calling loop function.
            }
            else
            {
                threadListOfCurrentResource2Holder.Add((int)id);


                // Wait until it is safe to enter.
                Console.WriteLine("{0} is requesting the mutex2",
                                  id);

                mut2.WaitOne();

                Console.WriteLine("{0} has entered the protected area and gain access to mutex2",
                                  id);
                //Thread.Sleep(1000);

                // Place code to access non-reentrant resources here.
                UseResource1(id);
                // Simulate some work.
                Thread.Sleep(500);

                Console.WriteLine("{0} is leaving the protected area",
                    id);

                // Release the Mutex.
                mut2.ReleaseMutex();
                Console.WriteLine("{0} has released the mutex2",
                    id);
            }
        }




    }
}
