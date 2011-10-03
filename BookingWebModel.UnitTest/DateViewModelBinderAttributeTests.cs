using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.WebModel;
using System.Web.Mvc;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class DateViewModelBinderAttributeTests
    {
        [Theory, AutoWebData]
        public void SutIsModelBinderAttribute(DateViewModelBinderAttribute sut)
        {
            Assert.IsAssignableFrom<CustomModelBinderAttribute>(sut);
        }
    }
}
