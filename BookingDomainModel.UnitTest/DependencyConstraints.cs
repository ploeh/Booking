using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class DependencyConstraints
    {
        [Theory]
        [InlineData("Castle.Core")]
        [InlineData("Castle.Windsor")]
        public void SutDoesNotReference(string assemblyName)
        {
            var references = typeof(IChannel<>).Assembly.GetReferencedAssemblies();
            Assert.False(references.Any(an => an.Name == assemblyName));
        }
    }
}
