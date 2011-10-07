using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ploeh.Samples.Booking.DomainModel;
using Newtonsoft.Json;

namespace Ploeh.Samples.Booking.JsonAntiCorruption
{
    public class JsonStreamObserver : IObserver<Stream>
    {
        private readonly IEnumerable<IQuickening> quickenings;
        private readonly IObserver<object> observer;
        private readonly JsonSerializer serializer;

        public JsonStreamObserver(IEnumerable<IQuickening> quickenings, IObserver<object> observer)
        {
            this.quickenings = quickenings;
            this.observer = observer;
            this.serializer = new JsonSerializer();
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Stream value)
        {
            using (var streamReader = new StreamReader(value))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    dynamic json = this.serializer.Deserialize(jsonReader);
                    var messages = from q in this.quickenings
                                   from m in (IEnumerable<IMessage>)q.Quicken(json)
                                   select m;
                    foreach (var m in messages)
                    {
                        this.observer.OnNext(m);
                    }
                }
            }
        }
    }
}
