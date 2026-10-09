using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class ConversionTests
    {
        [Fact]
        public void ToNullable_should_return_value_for_some()
        {
            int? result = Maybe.Some(5).ToNullable();

            Assert.Equal(5, result);
        }

        [Fact]
        public void ToNullable_should_return_null_for_none()
        {
            int? result = Maybe.None<int>().ToNullable();

            Assert.Null(result);
        }

        [Fact]
        public void ToNullable_should_throw_when_maybe_is_null()
        {
            Maybe<int> maybe = null;

            Assert.Throws<ArgumentNullException>(
                () => maybe.ToNullable());
        }

        [Fact]
        public void ToMaybe_should_return_some_when_nullable_has_value()
        {
            int? nullable = 5;

            AssertMaybe.IsSome(nullable.ToMaybe(), 5);
        }

        [Fact]
        public void ToMaybe_should_return_none_when_nullable_is_null()
        {
            int? nullable = null;

            AssertMaybe.IsNone(nullable.ToMaybe());
        }

        [Fact]
        public void Flatten_should_return_inner_some()
        {
            var nested = Maybe.Some(Maybe.Some(5));

            AssertMaybe.IsSome(nested.Flatten(), 5);
        }

        [Fact]
        public void Flatten_should_return_none_when_inner_is_none()
        {
            var nested = Maybe.Some(Maybe.None<int>());

            AssertMaybe.IsNone(nested.Flatten());
        }

        [Fact]
        public void Flatten_should_return_none_when_outer_is_none()
        {
            var nested = Maybe.None<Maybe<int>>();

            AssertMaybe.IsNone(nested.Flatten());
        }

        [Fact]
        public void Flatten_should_throw_when_maybe_is_null()
        {
            Maybe<Maybe<int>> nested = null;

            Assert.Throws<ArgumentNullException>(
                () => nested.Flatten());
        }
    }
}
