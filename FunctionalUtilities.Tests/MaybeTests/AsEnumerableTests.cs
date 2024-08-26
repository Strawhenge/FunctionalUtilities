using System.Linq;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class AsEnumerableTests
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
            var subject = Maybe.Some(item);

            var result = subject.AsEnumerable();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Same(item, result.Single());
        }
    }
}
