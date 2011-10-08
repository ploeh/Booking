using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public class DependencyConstraints
    {
        [Theory]
        [InlineData("Castle.Core")]
        [InlineData("Castle.Windsor")]
        public void SutDoesNotReference(string assemblyName)
        {
            var references = typeof(IQueue).Assembly.GetReferencedAssemblies();
            Assert.False(references.Any(an => an.Name == assemblyName));
        }
    }
}
