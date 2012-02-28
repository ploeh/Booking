using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.Samples.Booking.PersistenceModel;
using Newtonsoft.Json;
using System.IO;
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.JsonAntiCorruption
{
    public class JsonCapacityRepository : ICapacityRepository, IReader<DateTime, int>
    {
        private readonly IStoreWriter<DateTime> writer;
        private readonly IStoreReader<DateTime> reader;
        private readonly IEnumerable<IQuickening> quickenings;
        private readonly JsonSerializer serializer;

        public JsonCapacityRepository(IStoreWriter<DateTime> dateWriter, IStoreReader<DateTime> dateReader, IEnumerable<IQuickening> quickenings)
        {
            this.writer = dateWriter;
            this.reader = dateReader;
            this.quickenings = quickenings;
            this.serializer = new JsonSerializer();
        }

        public IEnumerable<Capacity> Read(DateTime date)
        {
            var capacity = this.GetEventsFor(date)
                .Aggregate(Capacity.Default, (c, e) => c.Reserve(e));

            return new[] { capacity };
        }

        public void Append(DateTime date, CapacityReservedEvent capacityReserved)
        {
            using (var stream = this.writer.OpenStreamFor(date))
            using (var writer = new StreamWriter(stream))
                this.serializer.Serialize(writer, capacityReserved.Envelop());
        }

        public int Query(DateTime arg)
        {
            return this.Read(arg).Single().Remaining;
        }

        private IEnumerable<CapacityReservedEvent> GetEventsFor(DateTime date)
        {
            var streams = this.reader.OpenStreamsFor(date);
            foreach (var stream in streams)
            {
                try
                {
                    using (var reader = new StreamReader(stream))
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        dynamic json = this.serializer.Deserialize(jsonReader);
                        var messages = from q in this.quickenings
                                       from m in (IEnumerable<IMessage>)q.Quicken(json)
                                       select m;
                        foreach (var m in messages)
                        {
                            yield return (CapacityReservedEvent)m;
                        }
                    }
                }
                finally
                {
                    stream.Dispose();
                }
            }
        }
    }
}
