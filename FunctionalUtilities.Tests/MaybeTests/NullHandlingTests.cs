using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class NullHandlingTests
    {
        [Fact]
        public void Map_should_throw_when_mapping_returns_null()
        {
            var some = Maybe.Some("text");

            Assert.Throws<ArgumentNullException>(
                () => some.Map<string>(_ => null));
        }

        [Fact]
        public void Implicit_conversion_should_throw_when_value_is_null()
        {
            Maybe<string> Convert(string value) => value;

            Assert.Throws<ArgumentNullException>(
                () => Convert(null));
        }

        [Fact]
        public void Combine_should_throw_when_function_returns_null()
        {
            var none = Maybe.None<string>();

            Assert.Throws<ArgumentNullException>(
                () => none.Combine(() => null));
        }

        [Fact]
        public void Do_should_throw_when_action_is_null()
        {
            AssertThrowsForSomeAndNone(maybe => maybe.Do(null));
        }

        [Fact]
        public void Map_should_throw_when_mapping_is_null()
        {
            AssertThrowsForSomeAndNone(maybe => maybe.Map<int>(null));
        }

        [Fact]
        public void Where_should_throw_when_predicate_is_null()
        {
            AssertThrowsForSomeAndNone(maybe => maybe.Where(null));
        }

        [Fact]
        public void WhereHas_should_throw_when_predicate_is_null()
        {
            AssertThrowsForSomeAndNone(maybe => maybe.WhereHas(null));
        }

        [Fact]
        public void Combine_should_throw_when_function_is_null()
        {
            AssertThrowsForSomeAndNone(maybe => maybe.Combine(null));
        }

        [Fact]
        public void Reduce_should_throw_when_fallback_is_null()
        {
            AssertThrowsForSomeAndNone(maybe => maybe.Reduce(null));
        }

        static void AssertThrowsForSomeAndNone(Action<Maybe<string>> act)
        {
            var maybes = new[]
            {
                Maybe.Some("text"),
                Maybe.None<string>()
            };

            foreach (var maybe in maybes)
            {
                Assert.Throws<ArgumentNullException>(() => act(maybe));
            }
        }
    }
}
