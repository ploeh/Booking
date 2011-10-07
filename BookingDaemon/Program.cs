using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.Persistence.FileSystem;
using System.IO;
using Ploeh.Samples.Booking.PersistenceModel;
using System.Threading;
using Ploeh.Samples.Booking.JsonAntiCorruption;
using Ploeh.Samples.Booking.DomainModel;
using System.Reactive.Subjects;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Ploeh.Samples.Booking.WebModel;
using Castle.Windsor;

namespace Ploeh.Samples.Booking.Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new WindsorContainer().Install(new DaemonWindsorInstaller()))
            {
                var q = container.Resolve<QueueConsumer>();

                RunUntilStopped(q);
            }
        }

        #region Console stuff
        private static void RunUntilStopped(QueueConsumer q)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    q.ConsumeAll();
                    Thread.Sleep(500);
                }
            }, tokenSource.Token);

            Console.WriteLine("Type \"quit\" or \"exit\" to exit.");
            do
            {
                Console.Write("> ");
            } while (DoNotExit());

            tokenSource.Cancel();
        }

        private static bool DoNotExit()
        {
            var line = Console.ReadLine().ToUpperInvariant();
            return line != "QUIT"
                && line != "EXIT";
        }
        #endregion
    }
}
