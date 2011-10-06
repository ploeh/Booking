using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System.IO;
using Moq;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public class PersistenceCustomization : CompositeCustomization
    {
        public PersistenceCustomization()
            : base(
                new StreamCustomization(),
                new StableFiniteSequenceCustomization(),
                new MultipleCustomization(),
                new AutoMoqCustomization())
        {
        }

        private class StreamCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() => fixture.CreateAnonymous<Mock<Stream>>().Object);
            }
        }
    }
}
