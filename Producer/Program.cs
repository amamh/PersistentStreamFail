using Entities;
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
            //var repository = new PriceRepository();
            //await Task.Run(() => repository.Start());


            var msg = new Price();
            msg.Ask = 19.382709503173828;
            msg.Mid = 19.372562408447266;
            msg.Bid = 19.377635955810547;
            msg.RIC = "IDUS.L";

            // this will stream the prices internally
            IStreamProvider streamProvider = GrainClient.GetStreamProvider("PSProvider");
            var streamId = new Guid("00000000-0000-0000-0000-000000000000");
            var stream = streamProvider.GetStream<IPrice>(streamId, "Price");
            await stream.OnNextAsync(msg);

            Console.ReadLine();
        }
    }
}
