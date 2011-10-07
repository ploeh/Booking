using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ploeh.Samples.Booking.Persistence.FileSystem
{
    public static class AutoDirectory
    {
        public static DirectoryInfo CreateIfAbsent(this DirectoryInfo directory)
        {
            if (!directory.Exists)
                directory.Create();

            return directory;
        }
    }
}
