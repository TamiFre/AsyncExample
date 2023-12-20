using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCollectionsAsync
{

    #region EventArgument
    /// <summary>
    /// this class stores the information about the progress of the task
    /// </summary>
    /// 
    class ProgressEventArgs :EventArgs
    {
        public int Percentage { get; set; }
        public ProgressEventArgs()
        { }  
        public ProgressEventArgs(int percent)
        {
            Percentage = percent;
        }
    }
    #endregion


    /// <summary>
    /// task to be executed. have a name and duration in miliseconds
    ///  </summary>
    ///  <event>
    ///  OnProgressUpdate - fires each time a progress in the task is made
    /// </event>
    /// <event>
    /// OnFinish - fires when task is completed
    /// </event>

    class TaskExecutor
    {
        private string name;
        private int timeInMiliSec;
        public TaskExecutor(string name, int ms)
        {
            this.timeInMiliSec = ms;
            this.name = name;
        }

        public string Name { get { return this.name; } }
        public void Start()
        {
            #region With Events
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(this.timeInMiliSec / 10);
                OnProgressUpdate?.Invoke(this, new((i+1) *10));
            }
            OnFinish?.Invoke(this,null);
            #endregion
        }

        #region async Start
        public async Task StartAsync()
        {
            for (int i = 0; i < 10; i++)
            {
               await Task.Delay(this.timeInMiliSec / 10);
                OnProgressUpdate?.Invoke(this, new((i + 1) * 10));
            }
            OnFinish?.Invoke(this, null);
        }
        #endregion
        #region With Events

        public event EventHandler<ProgressEventArgs> OnProgressUpdate;
        public event EventHandler OnFinish;
        #endregion
    }

    class Omlette : TaskExecutor
    {
        public Omlette(string name) : base(name, 15000)
        { }
    }

    class Cucumber : TaskExecutor
    {
        public Cucumber(string name) : base(name, 1000)
        { }
    }

    class Tomato : TaskExecutor
    {
        public Tomato(string name) : base(name, 1000)
        { }
    }

    class Toast : TaskExecutor
    {
        public Toast(string name) : base(name, 7500)
        { }
    }

}
