using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace CoreCollectionsAsync
{
    class Program
    {
        static async Task DemoAsync()
        {
            Console.WriteLine("Start Demo...");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Before...{i} {Environment.CurrentManagedThreadId}");
            }
            Task.Run(() => GetRandomAsync());
            Console.WriteLine($"After... ");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"After...{i} {Environment.CurrentManagedThreadId}");
            }
            Console.ReadKey();
        }

        static async Task<int> GetRandomAsync()
        {
            await Task.Delay(0);
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"GetRandomAsync...{i} {Environment.CurrentManagedThreadId}");

            }
            return r.Next(0, 100);
        }
        static async Task<int> DoSomethingIntAsync(int sec)
        {
            Console.WriteLine($"Lets Wait for {sec} seconds... ThreadID:{Environment.CurrentManagedThreadId}");
            await Task.Delay(1000 * sec);
            Console.WriteLine($"Done with waiting! ThreadID:{Environment.CurrentManagedThreadId}");
            return sec;
        }

        static async Task Start3()
        {
            Console.WriteLine($"Before running DoSomethingIntAsync for 2 sec ThreadID:{Environment.CurrentManagedThreadId}");
            int x = await DoSomethingIntAsync(2);
            Console.WriteLine($"x = {x}");
            Console.WriteLine($"After running DoSomethingIntAsync! ThreadID:{Environment.CurrentManagedThreadId}");

        }

        //********************************************************************
        static async Task DoSomethingAsync(int sec)
        {
            Console.WriteLine($"Lets Wait for {sec} seconds... ThreadID:{Environment.CurrentManagedThreadId}");
            await Task.Delay(1000 * sec);
            Console.WriteLine($"Done with waiting! ThreadID:{Environment.CurrentManagedThreadId}");
        }

        static void Start2()
        {
            Console.WriteLine($"Before running DoSomethingAsync for 2 sec ThreadID:{Environment.CurrentManagedThreadId}");
            Task t = DoSomethingAsync(2);
            Console.WriteLine($"After running DoSomethingAsync! ThreadID:{Environment.CurrentManagedThreadId}");
            t.Wait();
        }
        //********************************************************************
        static void DoSomething(int sec)
        {
            Console.WriteLine($"Lets Wait for {sec} seconds... ThreadID:{Environment.CurrentManagedThreadId}");
            Task t = Task.Delay(1000 * sec);
            t.Wait();
            Console.WriteLine($"Done with waiting! ThreadID:{Environment.CurrentManagedThreadId}");
        }

        static void Start()
        {
            Console.WriteLine($"Before running DoSomething for 2 sec ThreadID:{Environment.CurrentManagedThreadId}");
            DoSomething(2);
            Console.WriteLine($"After running DoSomething! ThreadID:{Environment.CurrentManagedThreadId}");
            Console.ReadKey();
        }
        //********************************************************************
        static void GoToSleep()
        {
            Console.WriteLine($"Going to sleep... ThreadID:{Environment.CurrentManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"Just woke up! ThreadID:{Environment.CurrentManagedThreadId}");
        }

        static void StartGoToSleep()
        {
            Console.WriteLine($"Running go to sleep ThreadID:{Environment.CurrentManagedThreadId}");
            Task t = Task.Run(GoToSleep);
            Console.WriteLine($"After running go to sleep ThreadID:{Environment.CurrentManagedThreadId}");
            t.Wait();


        }


        static void Main(string[] args)
        {

            //1. Prepare Omlette with no progress bar
            // DelegateAndEventsDemo.RunDemo_1();

            //2. Prepare Omlette with Progress and Finish Events
            //DelegateAndEventsDemo.RunDemo_2();

            //3. Prepare a full breakfast! no threads
            // BreakfastWIthThreads.MakeBreakfastDemo_1();
            //BreakfastWIthThreads.MakeBreakfastDemo_2();

            //4. Prepare a full breakfast using async methods! 

            BreakfastWIthThreads.MakeBreakfastDemoAsync_5().Wait();
           
            

         
            //5. Prepare a full breakfast ysing async and a Any method + list of tasks            
            //BreakfastWIthThreads.MakeBreakfastDemoAsync_5().Wait();

          
        }


    } 
}
