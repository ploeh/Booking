using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;
using System.Web.Mvc;
using System.Globalization;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class WebModelCustomization : CompositeCustomization
    {
        public WebModelCustomization()
            : base(
                new SpecimenBuilderComposerCustomization(),
                new MvcCustomization(),
                new AutoMoqCustomization())
        {
        }

        private class SpecimenBuilderComposerCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Inject<ISpecimenBuilderComposer>(fixture);
            }
        }

        private class MvcCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<ModelBindingContext>(c => c
                    .Without(x => x.Model)
                    .Without(x => x.ModelType));
                fixture.Inject(CultureInfo.CurrentCulture);
            }
        }
    }
}
