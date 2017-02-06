using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndAsyncExample.Models
{
    public class TimeEventArgs : EventArgs
    {
        public TimeEventArgs(DateTime time)
        {
            Hour = time.Hour;
            Minute = time.Minute;
            Second = time.Second;
        }

        public int Second { get; }

        public int Minute { get; }

        public int Hour { get; }

        public string Time => $"{Hour}:{Minute}:{Second}";
    }
}
