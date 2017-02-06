using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAndAsyncExample.Models
{
    // Co
    public class AsyncClass
    {
        Random rnd = new Random(1337);
        public async void Run()
        {
            var result = await Task.Run(async () =>
            {
                Console.WriteLine("Start first task");
                await Task.Delay(2000);
                Console.WriteLine("End first task");
                return rnd.Next();
            }).ContinueWith(async x =>
            {
                Console.WriteLine($"Start second task - received: {x.Result}");
                var tasks = new List<Task<string>>
                {
                    new Task<string>(() =>
                    {
                        Task.Delay(rnd.Next(3, 8) * 1000).Wait();
                        Console.WriteLine("Finished first of group");
                        return "first";
                    }),
                    new Task<string>( (Func<string>)( () =>
                    {
                        Task.Delay(rnd.Next(3, 8) * 1000).Wait();
                        Console.WriteLine("Finished second of group");
                        return "second";
                    })),
                    new Task<string>(() =>
                    {
                        Task.Delay(rnd.Next(2, 4) * 1000).Wait();
                        Console.WriteLine("Finished third of group");
                        return "third";
                    })
                };
                await Task.Run(() =>
                {
                    tasks.ForEach(t => t.Start());
                });
                var inner = await Task.WhenAll(tasks);

                Console.WriteLine("Finished group");
                return inner;
            }).Result;
            Console.WriteLine($"Result: {result.Aggregate("",(s,x) => $"{s}{x}, ")}");
        }
    }
}
