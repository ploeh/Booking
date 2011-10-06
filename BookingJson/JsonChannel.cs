using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;
using System.IO;
using Newtonsoft.Json;
using Ploeh.Samples.Booking.PersistenceModel;

namespace Ploeh.Samples.Booking.JsonAntiCorruption
{
    public class JsonChannel<T> : IChannel<T> where T : IMessage
    {
        private readonly IStoreWriter<T> store;
        private readonly JsonSerializer serializer;

        public JsonChannel(IStoreWriter<T> store)
        {
            this.store = store;
            this.serializer = new JsonSerializer();
        }

        public void Send(T message)
        {
            using (var stream = this.store.OpenStreamFor(message))
            using (var writer = new StreamWriter(stream))
                this.serializer.Serialize(writer, message.Envelop());
        }
    }
}
