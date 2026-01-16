using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class WhereHasTests
    {
        [Fact]
        public void None_should_return_false()
        {
            var maybe = Maybe.None<string>();

            Assert.False(
                maybe.WhereHas(s => s.Contains("abc")));
        }

        [Fact]
        public void Some_should_return_false_when_predicate_is_false()
        {
            var maybe = Maybe.Some("123");

            Assert.False(
                maybe.WhereHas(s => s.Contains("abc")));
        }
        
        [Fact]
        public void Some_should_return_true_when_predicate_is_true()
        {
            var maybe = Maybe.Some("abc123");

            Assert.True(
                maybe.WhereHas(s => s.Contains("abc")));
        }
    }
}