using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.WebModel;
using System.Web.Mvc;
using Ploeh.AutoFixture.Xunit;
using Moq;
using Ploeh.Samples.Booking.DomainModel;

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
        public void GetReturnsInstance(HomeController sut)
        {
            ViewResult actual = sut.Get();
            Assert.NotNull(actual);
        }

        [Theory, AutoWebData]
        public void GetReturnsCorrectModelType(HomeController sut)
        {
            var actual = sut.Get();
            Assert.IsAssignableFrom<IEnumerable<string>>(actual.Model);
        }

        [Theory, AutoWebData]
        public void GetReturnsCorrectModel(
            [Frozen]Mock<IReader<Month, IEnumerable<string>>> readerStub,
            string[] dates,
            HomeController sut)
        {
            var start = DateTime.Now;
            readerStub
                .Setup(r => r.Query(It.Is<Month>(m => start.Year <= m.Year && m.Year <= DateTime.Now.Year && start.Month <= m.MonthNumber && m.MonthNumber <= DateTime.Now.Month)))
                .Returns(dates);
            ViewResult result = sut.Get();
            Assert.Equal(dates, result.ViewData.Model);
        }
    }
}
