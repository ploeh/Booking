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
    public class DateViewModelBinderTests
    {
        [Theory, AutoWebData]
        public void SutIsModelBinder(DateViewModelBinder sut)
        {
            Assert.IsAssignableFrom<IModelBinder>(sut);
        }
    }
}
