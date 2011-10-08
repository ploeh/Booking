using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class DependencyConstraints
    {
        [Theory]
        [InlineData("Castle.Core")]
        [InlineData("Castle.Windsor")]
        public void SutDoesNotReference(string assemblyName)
        {
            var references = typeof(HomeController).Assembly.GetReferencedAssemblies();
            Assert.False(references.Any(an => an.Name == assemblyName));
        }
    }
}
