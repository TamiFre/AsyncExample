using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCollectionsAsync
{
    class DelegateAndEventsDemo
    {
        public static void RunDemo_1()
        {
            Console.WriteLine("Start preparing myOmlette...");
            Omlette oml = new Omlette("the Omlette");
            oml.Start();
            Console.WriteLine("myOmlette is Ready...");
        }

        public static void RunDemo_2()
        {
            Console.WriteLine("Start preparing myOmlette...");
            Omlette oml = new Omlette("the Omlette");
            //Register to get the event!
            oml.OnProgressUpdate += Progress;
            oml.OnFinish += Finish;
            //Start preparing
            oml.Start();
        }

        //The event OnProgressUpdate will fire this function! 


        public static void RunDemo_3()
        {
            //Omlette
            Console.WriteLine("Start preparing myOmlette...");
            Omlette oml = new Omlette("theOmlette");
            //Register to get the event!
            oml.OnProgressUpdate += Progress;
            oml.OnFinish += Finish; 
            //Start preparing
            oml.Start();
        }

        static void Progress(Object sender, ProgressEventArgs e)
        {
            if (sender is TaskExecutor)
            {
                TaskExecutor obj = (TaskExecutor)sender;
                Console.WriteLine($"Progress for {obj.Name}: {e.Percentage}%");
            }
        }
        static void Finish(object sender, EventArgs e)
        {
            if (sender is TaskExecutor)
            {
                TaskExecutor obj = (TaskExecutor)sender;
                Console.WriteLine($"{obj.Name} has finished.");
            }
        }

        
    }
}
