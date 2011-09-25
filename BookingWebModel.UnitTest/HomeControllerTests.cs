using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.WebModel;
using System.Web.Mvc;
using Ploeh.Samples.Booking.WebUI.Controllers;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class HomeControllerTests
    {
        [Theory, AutoWebData]
        public void SutIsController(HomeController sut)
        {
            Assert.IsAssignableFrom<IController>(sut);
        }
    }
}
