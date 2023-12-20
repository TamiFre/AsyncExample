using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCollectionsAsync
{

    class BreakfastWIthThreads
    {
        
        public static void MakeBreakfastDemo_1()
        {
            DateTime start = DateTime.Now;
            //Prepare Omlette
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Omlette myOmlette = new Omlette("myOmlette");
            myOmlette.OnProgressUpdate += Progress;
            myOmlette.OnFinish += Finish;
            myOmlette.Start();

            //Prepare toast
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Toast toast = new Toast("toast");
            toast.OnProgressUpdate += Progress;
            toast.OnFinish += Finish;
            toast.Start();

            //Prepare first cucumbers
            Console.WriteLine($"Start preparing the first cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber1 = new Cucumber("first cucumber");
            cucumber1.OnProgressUpdate += Progress;
            cucumber1.OnFinish += Finish;
            cucumber1.Start();

            //Prepare second cucumbers
            Console.WriteLine($"Start preparing the second cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber2 = new Cucumber("second cucumber");
            cucumber2.OnProgressUpdate += Progress;
            cucumber2.OnFinish += Finish;
            cucumber2.Start();

            //Prepare tomato
            Console.WriteLine($"Start preparing the tomato at {DateTime.Now.ToString()}");
            Tomato tomato = new Tomato("tomato");
            tomato.OnProgressUpdate += Progress;
            tomato.OnFinish += Finish;
            tomato.Start();

            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }

        //The event OnProgressUpdate will fire this function! 
        static void Progress(Object sender, ProgressEventArgs e )
        {
            if (sender is TaskExecutor)
            {
                TaskExecutor obj = (TaskExecutor)sender;
                Console.WriteLine($"Progress for {obj.Name}: {e.Percentage}%");
            }
        }

        //The event OnFinish will fire this function! 
        static void Finish(Object sender,EventArgs e)
        {
            if (sender is TaskExecutor)
            {
                TaskExecutor obj = (TaskExecutor)sender;
                Console.WriteLine($"{obj.Name} is ready!");
            }
        }

#region Threads
        public static void MakeBreakfastDemo_2()
        {
            DateTime start = DateTime.Now;
            //Prepare Omlette
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Omlette myOmlette = new Omlette("myOmlette");
            myOmlette.OnProgressUpdate += Progress;
            myOmlette.OnFinish += Finish;
            Thread omleteThread = new Thread(myOmlette.Start);
            omleteThread.Start();
            
            //Prepare toast
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Toast toast = new Toast("toast");
            toast.OnProgressUpdate += Progress;
            toast.OnFinish += Finish;
            Thread toastThread = new Thread(toast.Start);
            toastThread.Start();

            //Prepare first cucumbers
            Console.WriteLine($"Start preparing the first cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber1 = new Cucumber("first cucumber");
            cucumber1.OnProgressUpdate += Progress;
            cucumber1.OnFinish += Finish;
            cucumber1.Start();

            //Prepare second cucumbers
            Console.WriteLine($"Start preparing the second cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber2 = new Cucumber("second cucumber");
            cucumber2.OnProgressUpdate += Progress;
            cucumber2.OnFinish += Finish;
            cucumber2.Start();

            //Prepare tomato
            Console.WriteLine($"Start preparing the tomato at {DateTime.Now.ToString()}");
            Tomato tomato = new Tomato("tomato");
            tomato.OnProgressUpdate += Progress;
            tomato.OnFinish += Finish;
            tomato.Start();

            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }
    

        static List<TaskExecutor> tasks = new List<TaskExecutor>();
        public static void MakeBreakfastDemo_3()
        {
            DateTime start = DateTime.Now;
            //Prepare Omlette
            //Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Omlette myOmlette = new Omlette("myOmlette");
            myOmlette.OnProgressUpdate += Progress;
            myOmlette.OnFinish += FinishWithRemoval;
            tasks.Add(myOmlette);
            Thread omleteThread = new Thread(myOmlette.Start);
            omleteThread.Start();

            //Prepare toast
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Toast toast = new Toast("toast");
            toast.OnProgressUpdate += Progress;
            toast.OnFinish += FinishWithRemoval;
            tasks.Add(toast);
            Thread toastThread = new Thread(toast.Start);
            toastThread.Start();

            //Prepare first cucumbers
            Console.WriteLine($"Start preparing the first cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber1 = new Cucumber("first cucumber");
            cucumber1.OnProgressUpdate += Progress;
            cucumber1.OnFinish += FinishWithRemoval;
            tasks.Add(cucumber1);
            cucumber1.Start();

            //Prepare second cucumbers
            Console.WriteLine($"Start preparing the second cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber2 = new Cucumber("second cucumber");
            cucumber2.OnProgressUpdate += Progress;
            tasks.Add(cucumber2);
            cucumber2.OnFinish += FinishWithRemoval;
            cucumber2.Start();

            //Prepare tomato
            Console.WriteLine($"Start preparing the tomato at {DateTime.Now.ToString()}");
            Tomato tomato = new Tomato("tomato");
            tomato.OnProgressUpdate += Progress;
            tomato.OnFinish += FinishWithRemoval;
            tasks.Add(tomato);
            tomato.Start();

            //wait for all tasks to be over!
            while (tasks.Count > 0) { }

            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }

        static void FinishWithRemoval(Object sender,EventArgs e)
        {
            if (sender is TaskExecutor)
            {
                TaskExecutor obj = (TaskExecutor)sender;
                tasks.Remove(obj);
                Console.WriteLine($"{obj.Name} is ready!");
            }
        }
        #endregion

        public static async Task  MakeBreakfastDemoAsync_4()
        {
            
            DateTime start = DateTime.Now;
            //Prepare Omlette
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Task<Omlette> omlTask = PrepareOmletteAsync();
            

            //Prepare toast
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Task < Toast > toastTask = PrepareToastAsync();

            //Prepare Salad
            Console.WriteLine($"Start preparing the Salad at {DateTime.Now.ToString()}");
            Task saladTask = Task.Run(PrepareSalad);


            //wait for all tasks to be over!
            await Task.WhenAll(omlTask, toastTask, saladTask);
            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }

        public static async Task MakeBreakfastDemoAsync_5()
        {
            DateTime start = DateTime.Now;
            //Prepare Omlette
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Task<Omlette> omlTask = PrepareOmletteAsync();


            //Prepare toast
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Task<Toast> toastTask = PrepareToastAsync();

            //Prepare Salad
            Console.WriteLine($"Start preparing the Salad at {DateTime.Now.ToString()}");
            Task saladTask = Task.Run(PrepareSalad);


            //wait for all tasks to be over!
            List<Task> taskList = new List<Task> { omlTask, toastTask, saladTask };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == omlTask)
                {
                    Omlette o = await omlTask;
                    Console.WriteLine($"{o.Name} is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Toast t = await toastTask;
                    Console.WriteLine($"{t.Name} is ready");
                }
                else if (finishedTask == saladTask)
                {
                    Console.WriteLine("Salad is ready");
                }
                taskList.Remove(finishedTask);
            }
            
            
            DateTime end = DateTime.Now;
            TimeSpan length = end - start;
            Console.WriteLine($"Breakfast is ready at {end.ToString()}");
            Console.WriteLine($"Total time in seconds: {length.TotalSeconds}");

        }

        static async Task<Omlette> PrepareOmletteAsync()
        {
            Console.WriteLine($"Start preparing the Omlette at {DateTime.Now.ToString()}");
            Omlette myOmlette = new Omlette("myOmlette");
            myOmlette.OnProgressUpdate += Progress;
            await myOmlette.StartAsync();
            Console.WriteLine("Omlete is ready");
            return myOmlette;
        }

        static async Task<Toast> PrepareToastAsync()
        {
            Console.WriteLine($"Start preparing the toast at {DateTime.Now.ToString()}");
            Toast toast = new Toast("toast");
            toast.OnProgressUpdate += Progress;
            await toast.StartAsync();
            Console.WriteLine("Toast is ready!");
            return toast;
        }

        static void PrepareSalad()
        {
            //Prepare first cucumbers
            Console.WriteLine($"Start preparing the first cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber1 = new Cucumber("first cucumber");
            cucumber1.OnProgressUpdate += Progress;
            cucumber1.Start();

            //Prepare second cucumbers
            Console.WriteLine($"Start preparing the second cucumber at {DateTime.Now.ToString()}");
            Cucumber cucumber2 = new Cucumber("second cucumber");
            cucumber2.OnProgressUpdate += Progress;
            cucumber2.Start();

            //Prepare tomato
            Console.WriteLine($"Start preparing the tomato at {DateTime.Now.ToString()}");
            Tomato tomato = new Tomato("tomato");
            tomato.OnProgressUpdate += Progress;
            tomato.Start();
        }
    }


}
