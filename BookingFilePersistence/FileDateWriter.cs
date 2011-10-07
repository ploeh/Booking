using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.Booking.PersistenceModel;
using System.IO;

namespace Ploeh.Samples.Booking.Persistence.FileSystem
{
    public class FileDateWriter : IStoreWriter<DateTime>
    {
        private readonly DirectoryInfo directory;
        private readonly string extension;

        public FileDateWriter(DirectoryInfo directory, string extension)
        {
            this.directory = directory;
            this.extension = extension;
        }

        public Stream OpenStreamFor(DateTime item)
        {
            var dateDirectory = new DirectoryInfo(
                Path.Combine(
                    this.directory.FullName,
                    item.ToString("yyyyMMdd")));

            if (!dateDirectory.Exists)
                dateDirectory.Create();

            var fileName = Guid.NewGuid().ToString();
            var path = Path.ChangeExtension(
                Path.Combine(
                    dateDirectory.FullName,
                    fileName),
                extension);
            return File.Open(path, FileMode.Create);
        }
    }
}
