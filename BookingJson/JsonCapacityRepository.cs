using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.Samples.Booking.PersistenceModel;
using Newtonsoft.Json;
using System.IO;

namespace Ploeh.Samples.Booking.JsonAntiCorruption
{
    public class JsonCapacityRepository : ICapacityRepository
    {
        private readonly IStoreWriter<DateTime> writer;
        private readonly JsonSerializer serializer;

        public JsonCapacityRepository(IStoreWriter<DateTime> dateWriter)
        {
            this.writer = dateWriter;
            this.serializer = new JsonSerializer();
        }

        public IEnumerable<Capacity> Read(DateTime date)
        {
            yield break;
        }

        public void Write(DateTime date, CapacityReservedEvent capacityReserved)
        {
            using (var stream = this.writer.OpenStreamFor(date))
            using (var writer = new StreamWriter(stream))
                this.serializer.Serialize(writer, capacityReserved.Envelop());
        }
    }
}
