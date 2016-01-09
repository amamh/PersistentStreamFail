using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers.Streams.Common;
using Orleans.Streams;

namespace Client
{
    public class TestObserver : IAsyncObserver<int>
    {
        public async Task Subscribe()
        {
            IStreamProvider streamProvider = GrainClient.GetStreamProvider("PSProvider");
            var streamId = new Guid("00000000-0000-0000-0000-000000000000");
            var stream = streamProvider.GetStream<int>(streamId, "Price");
            _handle = await stream.SubscribeAsync(this, new PipeStreamProvider.SimpleSequenceToken(0));
        }

        public Task Unsubscribe()
        {
            return _handle.UnsubscribeAsync();
        }

        public Task OnNextAsync(int item, StreamSequenceToken token = null)
        {
            Console.WriteLine($"I received {item}");
            ReceivedMessage = true;
            return TaskDone.Done;
        }

        public Task OnCompletedAsync()
        {
            return TaskDone.Done;
        }

        public Task OnErrorAsync(Exception ex)
        {
            Console.WriteLine(ex);
            return TaskDone.Done;
        }

        public bool ReceivedMessage = false;
        private StreamSubscriptionHandle<int> _handle;
    }
}