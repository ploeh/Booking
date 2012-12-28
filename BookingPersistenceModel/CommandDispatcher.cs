using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.PersistenceModel
{
    public class CommandDispatcher<T> : IObserver<object>
    {
        private readonly ICommandHandler<T> commandHandler;

        public CommandDispatcher(ICommandHandler<T> commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(object value)
        {
            if (value is T)
                this.commandHandler.Execute((T)value);
        }
    }
}
