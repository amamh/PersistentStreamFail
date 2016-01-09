using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Orleans.GrainClient.Initialize("ClientConfiguration.xml");
                    break;
                }
                catch (Exception)
                {
                    Task.Delay(500).Wait();
                }
            }
            Task.Delay(5000).Wait();
            Run();

            Console.ReadLine();
        }

        private static async void Run()
        {
            IStreamProvider streamProvider = GrainClient.GetStreamProvider("PSProvider");
            var streamId = new Guid("00000000-0000-0000-0000-000000000000");
            var stream = streamProvider.GetStream<int>(streamId, "Price");
            await stream.OnNextAsync(0);

            Console.ReadLine();
        }
    }
}
