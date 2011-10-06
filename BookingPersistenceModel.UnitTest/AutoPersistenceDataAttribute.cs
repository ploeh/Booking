using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture.Xunit;
using Ploeh.AutoFixture;

namespace Ploeh.Samples.Booking.PersistenceModel.UnitTest
{
    public class AutoPersistenceDataAttribute : AutoDataAttribute
    {
        public AutoPersistenceDataAttribute()
            : base(new Fixture().Customize(new PersistenceCustomization()))
        {
        }
    }
}
