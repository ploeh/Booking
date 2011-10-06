using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public class DomainCustomization : CompositeCustomization
    {
        public DomainCustomization()
            : base(
                new AutoMoqCustomization())
        {
        }
    }
}
