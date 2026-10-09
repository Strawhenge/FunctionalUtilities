using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class OperatorTests
    {
        [Fact]
        public void Implicit_conversion_should_return_some()
        {
            Maybe<Exception> maybe = new Exception();

            AssertMaybe.IsSome(maybe);
        }

        [Fact]
        public void Explicit_conversion_should_return_value_for_some()
        {
            const string text = "This is the string.";

            var maybe = Maybe.Some(text);

            var castedText = (string)maybe;

            Assert.Equal(text, castedText);
        }

        [Fact]
        public void Explicit_conversion_should_throw_for_none()
        {
            var maybe = Maybe.None<string>();

            Assert.Throws<InvalidCastException>(() =>
            {
                _ = (string)maybe;
            });
        }
    }
}