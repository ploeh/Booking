using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.Samples.Booking.WebModel;

namespace Ploeh.Samples.Booking.Persistence.FileSystem
{
    public class FileMonthViewStore : IObserver<DateTime>, IReader<Month, IEnumerable<string>>
    {
        private readonly DirectoryInfo directory;
        private readonly string extension;

        public FileMonthViewStore(DirectoryInfo viewStoreDirectory, string extension)
        {
            this.directory = viewStoreDirectory;
            this.extension = extension;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DateTime value)
        {
            var monthDirectory = new DirectoryInfo(
                Path.Combine(
                    this.directory.FullName,
                    "MonthViews")).CreateIfAbsent();

            var fileName = Path.ChangeExtension(
                Path.Combine(
                    monthDirectory.FullName,
                    value.ToString("yyyyMM")),
                this.extension);
            var file = new FileInfo(fileName);
            if (!file.Exists)
                file.Create();

            using (var writer = file.AppendText())
                writer.WriteLine(value.ToString("yyyy.MM.dd"));
        }

        public IEnumerable<string> Query(Month arg)
        {
            var monthDirectory = new DirectoryInfo(
                Path.Combine(
                    this.directory.FullName,
                    "MonthViews"));

            if (!monthDirectory.Exists)
                return Enumerable.Empty<string>();

            var fileName = Path.ChangeExtension(
                Path.Combine(
                    monthDirectory.FullName,
                    arg.Year.ToString("D4") + arg.MonthNumber.ToString("D2")),
                this.extension);
            if (!File.Exists(fileName))
                return Enumerable.Empty<string>();

            return File.ReadAllLines(fileName);
        }
    }
}
