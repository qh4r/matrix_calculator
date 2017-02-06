using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsAndAsyncExample.Models
{
    public class Clock
    {
        public event EventHandler<TimeEventArgs> TimeUpdate;

        private Timer timer;

        public Clock()
        {
            timer = new Timer(this.TimerCallback, null, Timeout.Infinite, 1000);
        }

        public void Run()
        {
            timer.Change(0, 1000);
        }

        private void TimerCallback(object state)
        {
            TimeUpdate?.Invoke(this, new TimeEventArgs(DateTime.Now));
        }
    }
}
