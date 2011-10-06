using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Ploeh.Samples.Booking.PersistenceModel;

namespace Ploeh.Samples.Booking.Persistence.FileSystem
{
    public class FileQueue : IQueue
    {
        private readonly DirectoryInfo directory;
        private readonly string extension;

        public FileQueue(DirectoryInfo directory, string extension)
        {
            this.directory = directory;
            this.extension = extension;
        }

        public void Delete(Stream stream)
        {
            var fileStream = stream as FileStream;
            if (fileStream != null)
            {
                File.Delete(fileStream.Name);
            }
        }

        public IEnumerator<Stream> GetEnumerator()
        {
            while(true)
            {
                var file = this.directory.EnumerateFiles("*." + this.extension).FirstOrDefault();
                if (file == null)
                    yield break;

                yield return file.OpenRead();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
