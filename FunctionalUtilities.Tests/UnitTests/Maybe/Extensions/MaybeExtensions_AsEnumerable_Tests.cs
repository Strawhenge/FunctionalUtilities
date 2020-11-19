using System.Linq;
using Xunit;

namespace FunctionalUtilities.Tests.UnitTests.MaybeExtensions
{
    public class MaybeExtensions_AsEnumerable_Tests
    {
        [Fact]
        public void AsEnumerable_GivenNone_ShouldReturnEmptyEnumerable()
        {
            var subject = Maybe.None<object>();

            var result = subject.AsEnumerable();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void AsEnumerable_GivenSoMe_ShouldReturnEnumerableWithSingleItem()
        {
            var item = new object();
            var subject = Maybe.Some<object>(item);

            var result = subject.AsEnumerable();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Same(item, result.Single());
        }
    }
}
