using System;
using System.Threading.Tasks;
using Orleans;
using System.Collections.Generic;
using Orleans.Streams;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Retry till connected
            while (true)
            {
                try
                {
                    GrainClient.Initialize();
                    break;
                }
                catch (Exception)
                {
                    Task.Delay(500).Wait();
                }
            }
            // Delay
            Console.WriteLine("Waiting");
            Task.Delay(2000).Wait();
            Console.WriteLine("Starting");

            // Test: repeat till problem occurs

            var list = new List<TestObserver>();


            while (true)
            {
                var testObserver = new TestObserver();
                list.Add(testObserver);
                testObserver.Subscribe().Wait();
                var received = false;
                // wait for client to receive message
                for (int i = 0; i < 1000; i++)
                {
                    if (testObserver.ReceivedMessage)
                    {
                        received = true;
                        break;
                    }
                    Task.Delay(5).Wait();
                }
                //testObserver.Unsubscribe().Wait();
                if (!received)
                {
                    Console.WriteLine("Client did not receive the message.");
                    break;
                }

                Task.Delay(1500).Wait();
            }

            Console.ReadLine();
        }
    }
}