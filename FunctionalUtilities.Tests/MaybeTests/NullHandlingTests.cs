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

            var exception = Assert.Throws<ArgumentNullException>(
                () => some.Map<string>(_ => null));

            Assert.Equal("mapping", exception.ParamName);
            Assert.Contains("returned null", exception.Message);
        }

        [Fact]
        public void Implicit_conversion_should_throw_when_value_is_null()
        {
            Maybe<string> Convert(string value) => value;

            var exception = Assert.Throws<ArgumentNullException>(
                () => Convert(null));

            Assert.Equal("value", exception.ParamName);
            Assert.Contains("Maybe.NotNull", exception.Message);
        }

        [Fact]
        public void Combine_should_throw_when_function_returns_null()
        {
            var none = Maybe.None<string>();

            var exception = Assert.Throws<ArgumentNullException>(
                () => none.Combine(() => null));

            Assert.Equal("combineWith", exception.ParamName);
            Assert.Contains("returned null", exception.Message);
        }

        [Fact]
        public void Do_should_throw_when_action_is_null()
        {
            AssertThrowsForSomeAndNone("action", maybe => maybe.Do(null));
        }

        [Fact]
        public void Map_should_throw_when_mapping_is_null()
        {
            AssertThrowsForSomeAndNone("mapping", maybe => maybe.Map<int>(null));
        }

        [Fact]
        public void Where_should_throw_when_predicate_is_null()
        {
            AssertThrowsForSomeAndNone("predicate", maybe => maybe.Where(null));
        }

        [Fact]
        public void WhereHas_should_throw_when_predicate_is_null()
        {
            AssertThrowsForSomeAndNone("predicate", maybe => maybe.WhereHas(null));
        }

        [Fact]
        public void Combine_should_throw_when_function_is_null()
        {
            AssertThrowsForSomeAndNone("combineWith", maybe => maybe.Combine(null));
        }

        [Fact]
        public void Reduce_should_throw_when_fallback_is_null()
        {
            AssertThrowsForSomeAndNone("fallback", maybe => maybe.Reduce(null));
        }

        static void AssertThrowsForSomeAndNone(string paramName, Action<Maybe<string>> act)
        {
            var maybes = new[]
            {
                Maybe.Some("text"),
                Maybe.None<string>()
            };

            foreach (var maybe in maybes)
            {
                var exception = Assert.Throws<ArgumentNullException>(() => act(maybe));

                Assert.Equal(paramName, exception.ParamName);
            }
        }
    }
}
