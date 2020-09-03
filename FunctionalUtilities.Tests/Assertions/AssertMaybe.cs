namespace FunctionalUtilities.Tests
{
    public static class AssertMaybe
    {
        public static void Some<T>(Maybe<T> maybe) => maybe.Reduce(() => throw new IsNoneException());

        public static void None<T>(Maybe<T> maybe) => maybe.Do(x => throw new IsSomeException(x));
    }
}
