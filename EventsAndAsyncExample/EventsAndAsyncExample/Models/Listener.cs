using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndAsyncExample.Models
{
    public class Listener
    {
        private readonly string _name;
        private readonly Clock clock;

        public Listener(string name, Clock clock)
        {
            _name = name;
            this.clock = clock;
            this.clock.TimeUpdate += OnTimeUpdate;
        }

        ~Listener()
        {
            clock.TimeUpdate -= OnTimeUpdate;
        }

        private void OnTimeUpdate(object sender, TimeEventArgs timeEventArgs)
        {
            Console.WriteLine($"{_name} => {timeEventArgs.Time}");
        }
    }
}