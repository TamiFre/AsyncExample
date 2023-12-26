using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassesForExercise
{
    public class Battery
    {
        
        const int MAX_CAPACITY = 1000;
        private static Random r = new Random();
        //Add events to the class to notify upon threshhold reached and shut down!
        #region events
        public event EventHandler ReachThreshold;
        public event EventHandler ShutDown;
        #endregion
        private int Threshold { get; }
        public int Capacity { get; set; }
        public int Percent
        {
            get
            {
                return 100 * Capacity / MAX_CAPACITY;
            }
        }
        public Battery()
        {
            Capacity = MAX_CAPACITY;
            Threshold = 400;
        }

        public void Usage()
        {
            Capacity -= r.Next(50, 150);
            //Add calls to the events based on the capacity and threshhold
            #region Fire Events
            if (Capacity < Threshold)
            {
                OnReachThreshold();
            }
            if (Capacity == 0)
            {
                OnCarShutDownAlmost();
            }

            #endregion
        }

        //מפעיל את האיוונט 
        public void OnReachThreshold()
        {
            ReachThreshold?.Invoke(this, new EventArgs());
        }
        public void OnCarShutDownAlmost()
        {
            ShutDown?.Invoke(this, new EventArgs());
        }


    }

    class ElectricCar
    {
        public Battery Bat { get; set; }
        private int id;

        //Add event to notify when the car is shut down
        public event EventHandler OnCarShutDown;

        public ElectricCar(int id)
        {
            this.id = id;
            Bat = new Battery();
            //רוטשם את המכונית לאיוונט
            #region Register to battery events
            Bat.ReachThreshold += WhenLowBattery;
            Bat.ShutDown += WhenAlmostMadmim;
            #endregion
            this.OnCarShutDown += WhenShutDown;
        }

        //פעולות שאומרות מה כל איוונט יכתוב או יעשה
        public void WhenLowBattery(object sender, EventArgs args)
        {
            Console.WriteLine("Warning! Low Battery Detected");
            Console.Beep(440,2000);
        }

        public void WhenAlmostMadmim(object sender, EventArgs args)
        {
            Console.WriteLine("Car is about to shut down. Charge please");
        }



        public void StartEngine()
        {
            while (Bat.Capacity > 0)
            {
                Console.WriteLine($"{this} {Bat.Percent}% Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Bat.Usage();
            }
            OnCarShutDownActivate();
        }

        public void OnCarShutDownActivate()
        {
            OnCarShutDown?.Invoke(this, new EventArgs());

        }
        public void WhenShutDown(object sender, EventArgs args)
        {
            Console.WriteLine("Car Shut Down");
        }

        //Add code to Define and implement the battery event implementations
        #region events implementation
        #endregion

        public override string ToString()
        {
            return $"Car: {id}";
        }

    }

}
