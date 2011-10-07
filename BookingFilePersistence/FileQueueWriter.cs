using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.PersistenceModel;
using System.IO;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.Persistence.FileSystem
{
    public class FileQueueWriter<T> : IStoreWriter<T> where T : IMessage
    {
        private readonly DirectoryInfo directory;
        private readonly string extension;

        public FileQueueWriter(DirectoryInfo directory, string extension)
        {
            this.directory = directory;
            this.extension = extension;
        }

        public Stream OpenStreamFor(T item)
        {
            var fileName = item.Envelop().BodyType + item.Id;
            var path = Path.ChangeExtension(
                Path.Combine(
                    this.directory.FullName,
                    fileName),
                extension);
            return File.Open(path, FileMode.Create);
        }
    }
}
