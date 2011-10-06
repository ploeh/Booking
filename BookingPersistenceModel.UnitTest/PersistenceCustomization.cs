using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public class PersistenceCustomization : CompositeCustomization
    {
        public PersistenceCustomization()
            : base(new AutoMoqCustomization())
        {
        }
    }
}
