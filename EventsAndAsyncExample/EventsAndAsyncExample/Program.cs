using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventsAndAsyncExample.Models;

namespace EventsAndAsyncExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Clock();
            var listeners = new[]
            {
                new Listener("Listener 1", timer),
                new Listener("Listener 2", timer),
                new Listener("Listener 3", timer),
                new Listener("Listener 4", timer),
                new Listener("Listener 5", timer),
            };
            timer.Run();
            var asyncInstance = new AsyncClass();
            asyncInstance.Run();            
            Console.ReadKey();
        }
    }
}
