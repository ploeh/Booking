using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel;
using Ploeh.Samples.Booking.DomainModel;
using Castle.MicroKernel.Registration;
using Ploeh.Samples.Booking.PersistenceModel;

namespace Ploeh.Samples.Booking.Daemon
{
    public class ConsumerConvention : AbstractFacility
    {
        protected override void Init()
        {
            this.Kernel.HandlerRegistered += this.OnHandlerRegistered;
        }

        private void OnHandlerRegistered(IHandler handler, ref bool stateChanged)
        {
            var messageTypes = from t in handler.ComponentModel.Services
                               where t.IsInterface
                               && t.IsGenericType
                               && t.GetGenericTypeDefinition() == typeof(IConsumer<>)
                               select t.GetGenericArguments().Single();

            foreach (var t in messageTypes)
            {
                this.Kernel.Register(Component
                    .For<IObserver<object>>()
                    .ImplementedBy(typeof(Dispatcher<>).MakeGenericType(t)));
            }
        }
    }
}
