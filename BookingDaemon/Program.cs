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

namespace Ploeh.Samples.Booking.Daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueDirectory = 
                new DirectoryInfo(@"..\..\..\BookingWebUI\Queue").CreateIfAbsent();
            var singleSourceOfTruthDirectory = 
                new DirectoryInfo(@"..\..\..\BookingWebUI\SSoT").CreateIfAbsent();
            var viewStoreDirectory = 
                new DirectoryInfo(@"..\..\..\BookingWebUI\ViewStore").CreateIfAbsent();

            var extension = "txt";

            var fileDateStore = new FileDateStore(
                singleSourceOfTruthDirectory,
                extension);

            var quickenings = new IQuickening[]
            {
                new RequestReservationCommand.Quickening(),
                new ReservationAcceptedEvent.Quickening(),
                new ReservationRejectedEvent.Quickening(),
                new CapacityReservedEvent.Quickening(),
                new SoldOutEvent.Quickening()
            };

            var disposable = new CompositeDisposable();
            var messageDispatcher = new Subject<object>();
            disposable.Add(
                messageDispatcher.Subscribe(
                    new Dispatcher<RequestReservationCommand>(
                        new CapacityGate(
                            new JsonCapacityRepository(
                                fileDateStore,
                                fileDateStore,
                                quickenings),
                            new JsonChannel<ReservationAcceptedEvent>(
                                new FileQueueWriter<ReservationAcceptedEvent>(
                                    queueDirectory,
                                    extension)),
                            new JsonChannel<ReservationRejectedEvent>(
                                new FileQueueWriter<ReservationRejectedEvent>(
                                    queueDirectory,
                                    extension)),
                            new JsonChannel<SoldOutEvent>(
                                new FileQueueWriter<SoldOutEvent>(
                                    queueDirectory,
                                    extension))))));
            disposable.Add(
                messageDispatcher.Subscribe(
                    new Dispatcher<SoldOutEvent>(
                        new MonthViewUpdater(
                            new FileMonthViewStore(
                                viewStoreDirectory,
                                extension)))));

            var q = new QueueConsumer(
                new FileQueue(
                    queueDirectory,
                    extension),
                new JsonStreamObserver(
                    quickenings,
                    messageDispatcher));

            RunUntilStopped(q);
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
