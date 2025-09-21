namespace FunctionalUtilities
{
    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) => new Some<T>(value);

        public static Maybe<T> None<T>() => FunctionalUtilities.None<T>.Instance;

        public static Maybe<T> NotNull<T>(T value) => ReferenceEquals(value, null)
            ? None<T>()
            : Some(value);

        public static Maybe<string> NotNullOrEmpty(string value) => string.IsNullOrEmpty(value)
            ? None<string>()
            : Some(value);

        public static Maybe<string> NotNullOrWhiteSpace(string value) => string.IsNullOrWhiteSpace(value)
            ? None<string>()
            : Some(value);
    }
}