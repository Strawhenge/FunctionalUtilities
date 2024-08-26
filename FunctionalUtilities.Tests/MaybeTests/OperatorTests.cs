using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class OperatorTests
    {
        [Fact]
        public void ImplicitOperator_ShouldCastToMaybe()
        {
            Maybe<Exception> maybe = new Exception();

            AssertMaybe.IsSome(maybe);
        }

        [Fact]
        public void ExplicitOperator_WhenSome_ShouldCast()
        {
            const string text = "This is the string.";

            var maybe = Maybe.Some(text);

            var castedText = (string)maybe;

            Assert.Equal(text, castedText);
        }

        [Fact]
        public void ExplicitOperator_WhenNone_ShouldThrow()
        {
            var maybe = Maybe.None<string>();

            Assert.ThrowsAny<Exception>(() =>
            {
                _ = (string)maybe;
            });
        }
    }
}