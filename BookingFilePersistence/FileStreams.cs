using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Ploeh.Samples.Booking.Persistence.FileSystem
{
    public class FileStreams : IEnumerable<Stream>
    {
        private readonly DirectoryInfo directory;
        private readonly string extension;

        public FileStreams(DirectoryInfo directory, string extension)
        {
            this.directory = directory;
            this.extension = extension;
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
