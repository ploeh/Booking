using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.WebModel;
using System.Web.Mvc;
using Moq;
using System.Globalization;
using Ploeh.SemanticComparison.Fluent;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class DateViewModelBinderTests
    {
        [Theory, AutoWebData]
        public void SutIsModelBinder(DateViewModelBinder sut)
        {
            Assert.IsAssignableFrom<IModelBinder>(sut);
        }

        [Theory, AutoWebData]
        public void BindModelReturnsCorrectResult(DateViewModelBinder sut,
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            DateTime dateTime,
            CultureInfo culture)
        {
            var rawValue = dateTime.ToString("yyyy.MM.dd");
            Mock.Get(bindingContext.ValueProvider)
                .Setup(vp => vp.GetValue("id"))
                .Returns(new ValueProviderResult(rawValue, rawValue, culture));

            var actual = sut.BindModel(controllerContext, bindingContext);

            var model = Assert.IsAssignableFrom<DateViewModel>(actual);
            dateTime.AsSource().OfLikeness<DateViewModel>().ShouldEqual(model);
        }
    }
}
