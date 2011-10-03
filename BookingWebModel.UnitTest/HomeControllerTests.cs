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
    public class HomeControllerTests
    {
        [Theory, AutoWebData]
        public void SutIsController(HomeController sut)
        {
            Assert.IsAssignableFrom<IController>(sut);
        }

        [Theory, AutoWebData]
        public void IndexReturnsInstance(HomeController sut)
        {
            ViewResult actual = sut.Index();
            Assert.NotNull(actual);
        }

        [Theory, AutoWebData]
        public void IndexReturnsCorrectModelType(HomeController sut)
        {
            var actual = sut.Index();
            Assert.IsAssignableFrom<IEnumerable<string>>(actual.Model);
        }
    }
}
