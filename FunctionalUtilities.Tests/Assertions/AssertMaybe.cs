using Xunit;

namespace FunctionalUtilities.Tests
{
    public static class AssertMaybe
    {
        public static void IsSome<T>(Maybe<T> maybe)
        {
            Assert.NotNull(maybe);
            maybe.Reduce(() => throw new IsNoneException());
        }

        public static void IsSome<T>(Maybe<T> maybe, T expectedValue)
        {
            Assert.NotNull(maybe);
            var value = maybe.Reduce(() => throw new IsNoneException());
            Assert.Equal(expectedValue, value);
        }

        public static void IsNone<T>(Maybe<T> maybe)
        {
            Assert.NotNull(maybe);
            maybe.Do(x => throw new IsSomeException(x!));
        }
    }
}