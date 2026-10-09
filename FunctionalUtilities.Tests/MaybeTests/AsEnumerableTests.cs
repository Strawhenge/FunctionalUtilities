using System.Linq;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class AsEnumerableTests
    {
        [Fact]
        public void AsEnumerable_should_return_empty_enumerable_for_none()
        {
            var subject = Maybe.None<object>();

            var result = subject.AsEnumerable();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void AsEnumerable_should_return_enumerable_with_single_item_for_some()
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
