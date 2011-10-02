using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;

namespace Ploeh.Samples.Booking.WebModel.UnitTest
{
    public class WebModelCustomization : CompositeCustomization
    {
        public WebModelCustomization()
            : base(
                new SpecimenBuilderComposerCustomization(),
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
    }
}
