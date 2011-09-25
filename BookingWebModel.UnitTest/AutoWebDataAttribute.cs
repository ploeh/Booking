using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture.Xunit;
using Ploeh.AutoFixture;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class AutoWebDataAttribute : AutoDataAttribute
    {
        public AutoWebDataAttribute()
            : base(new Fixture().Customize(new WebModelCustomization()))
        {
        }
    }
}
