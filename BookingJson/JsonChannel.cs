using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;
using System.IO;
using Newtonsoft.Json;

namespace Ploeh.Samples.Booking.JsonAntiCorruption
{
    public class JsonChannel<T> : IChannel<T> where T : IMessage
    {
        private readonly IStore<T> store;
        private readonly JsonSerializer serializer;

        public JsonChannel(IStore<T> store)
        {
            this.store = store;
            this.serializer = new JsonSerializer();
        }

        public void Send(T message)
        {
            using (var stream = this.store.GetStreamFor(message))
            using (var writer = new StreamWriter(stream))
                this.serializer.Serialize(writer, message);
        }
    }
}
