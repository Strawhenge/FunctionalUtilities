using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class CombineTests
    {
        [Fact]
        public void Some_combined_with_some_should_return_some_with_original_value()
        {
            var value = new object();
            var some = Maybe.Some(value);

            var result = some.Combine(() => Maybe.Some(new object()));

            AssertMaybe.IsSome(result, expectedValue: value);
        }

        [Fact]
        public void Some_combined_with_none_should_return_some_with_original_value()
        {
            var value = new object();
            var some = Maybe.Some(value);

            var result = some.Combine(Maybe.None<object>);

            AssertMaybe.IsSome(result, expectedValue: value);
        }

        [Fact]
        public void None_combined_with_some_should_return_some_with_new_value()
        {
            var none = Maybe.None<object>();

            var value = new object();
            var result = none.Combine(() => Maybe.Some(value));

            AssertMaybe.IsSome(result, expectedValue: value);
        }

        [Fact]
        public void None_combined_with_none_should_return_none()
        {
            var none = Maybe.None<object>();

            var result = none.Combine(Maybe.None<object>);

            AssertMaybe.IsNone(result);
        }
    }
}