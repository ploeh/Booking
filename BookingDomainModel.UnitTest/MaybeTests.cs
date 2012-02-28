using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Extensions;
using Xunit;
using Ploeh.Samples.Booking.DomainModel;
using Ploeh.AutoFixture.Xunit;
using System.Collections;

namespace Ploeh.Samples.Booking.DomainModel.UnitTest
{
    public abstract class MaybeTests<T>
    {
        [Theory, AutoDomainData]
        public void SutIsEnumerable(Maybe<T> sut)
        {
            Assert.IsAssignableFrom<IEnumerable<T>>(sut);
        }

        [Theory, AutoDomainData]
        public void CreatedWithModestConstructorIsNonGenericallyEmpty(
            [Modest]Maybe<T> sut)
        {
            Assert.Empty(sut);
        }

        [Theory, AutoDomainData]
        public void CreatedWithModestConstructorIsGenericallyEmpty(
            [Modest]Maybe<T> sut)
        {
            Assert.False(sut.Any());
        }

        [Theory, AutoDomainData]
        public void CreatedWithGreedyConstructorYieldsCorrectItem(
            [Frozen]T expected,
            [Greedy]Maybe<T> sut)
        {
            Assert.Equal(expected, sut.Single());
        }

        [Theory, AutoDomainData]
        public void CreatedWithGreedyConstructorYieldsCorrectItemOnNonGenericInterface(
            [Frozen]T expected,
            [Greedy]Maybe<T> sut)
        {
            IEnumerable actual = sut;
            Assert.Equal(expected, actual.OfType<T>().Single());
        }
    }

    public class MaybeTestsOfObject : MaybeTests<object> { }
    public class MaybeTestsOfString : MaybeTests<string> { }
    public class MaybeTestsOfInt32 : MaybeTests<int> { }
    public class MaybeTestsOfGuid : MaybeTests<Guid> { }
    public class MaybeTestsOfVersion : MaybeTests<Version> { }
}
