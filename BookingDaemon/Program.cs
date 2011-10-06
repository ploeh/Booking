using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.Persistence.FileSystem;
using System.IO;
using Ploeh.Samples.Booking.PersistenceModel;
using System.Threading;

namespace Ploeh.Samples.Booking.Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new PollingConsumer(
                new FileStreams(
                    new DirectoryInfo(@"C:\Users\mark\Documents\Presentations\Conventions\Booking\BookingWebUI\Queue"),
                    "txt"),
                new NullObserver<Stream>());
            while (true)
            {
                q.ConsumeSequence();
                Thread.Sleep(500);
            }
        }

        private class NullObserver<T> : IObserver<T>
        {
            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(T value)
            {
            }
        }
    }
}
