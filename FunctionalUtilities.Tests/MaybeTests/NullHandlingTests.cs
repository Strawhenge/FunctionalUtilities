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
    }
}
