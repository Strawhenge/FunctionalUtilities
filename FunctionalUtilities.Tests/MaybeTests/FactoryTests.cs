using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class FactoryTests
    {
        [Fact]
        public void Some_should_throw_when_value_is_null()
        {
            Assert.Throws<ArgumentNullException>(
                () => Maybe.Some<string>(null));
        }

        [Fact]
        public void NotNull_should_return_some_when_value_is_not_null()
        {
            AssertMaybe.IsSome(Maybe.NotNull("text"), "text");
        }

        [Fact]
        public void NotNull_should_return_none_when_value_is_null()
        {
            AssertMaybe.IsNone(Maybe.NotNull<string>(null));
        }

        [Fact]
        public void NotNull_should_return_some_for_a_value_type()
        {
            AssertMaybe.IsSome(Maybe.NotNull(0), 0);
        }

        [Fact]
        public void NotNull_should_return_none_when_nullable_value_type_is_null()
        {
            int? value = null;

            AssertMaybe.IsNone(Maybe.NotNull(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NotNullOrEmpty_should_return_none_when_null_or_empty(string value)
        {
            AssertMaybe.IsNone(Maybe.NotNullOrEmpty(value));
        }

        [Theory]
        [InlineData("text")]
        [InlineData(" ")]
        public void NotNullOrEmpty_should_return_some_otherwise(string value)
        {
            AssertMaybe.IsSome(Maybe.NotNullOrEmpty(value), value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("\t")]
        public void NotNullOrWhiteSpace_should_return_none_when_null_empty_or_white_space(string value)
        {
            AssertMaybe.IsNone(Maybe.NotNullOrWhiteSpace(value));
        }

        [Fact]
        public void NotNullOrWhiteSpace_should_return_some_otherwise()
        {
            AssertMaybe.IsSome(Maybe.NotNullOrWhiteSpace(" text "), " text ");
        }
    }
}
