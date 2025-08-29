namespace FunctionalUtilities
{
    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) => new Some<T>(value);

        public static Maybe<T> None<T>() => FunctionalUtilities.None<T>.Instance;

        public static Maybe<T> FromNullable<T>(T value) => ReferenceEquals(value, null)
            ? None<T>()
            : Some(value);
    }
}