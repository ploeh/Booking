using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.PersistenceModel;
using System.IO;

namespace Ploeh.Samples.Booking.Persistence.FileSystem
{
    public class FileDateStore : IStoreWriter<DateTime>, IStoreReader<DateTime>
    {
        private readonly DirectoryInfo directory;
        private readonly string extension;

        public FileDateStore(DirectoryInfo directory, string extension)
        {
            this.directory = directory;
            this.extension = extension;
        }

        public Stream OpenStreamFor(DateTime item)
        {
            var dateDirectory = this.GetDirectory(item);

            var fileName = Guid.NewGuid().ToString();
            var path = Path.ChangeExtension(
                Path.Combine(
                    dateDirectory.FullName,
                    fileName),
                extension);
            return File.Open(path, FileMode.Create);
        }

        public IEnumerable<Stream> OpenStreamsFor(DateTime item)
        {
            var dateDirectory = this.GetDirectory(item);

            return from f in dateDirectory.EnumerateFiles("*." + extension)
                   orderby f.CreationTime
                   select f.OpenRead();
        }

        private DirectoryInfo GetDirectory(DateTime item)
        {
            var dateDirectory = new DirectoryInfo(
                Path.Combine(
                    this.directory.FullName,
                    item.ToString("yyyyMMdd")));

            if (!dateDirectory.Exists)
                dateDirectory.Create();
            return dateDirectory;
        }
    }
}
