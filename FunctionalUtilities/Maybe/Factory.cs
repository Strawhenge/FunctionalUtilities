namespace FunctionalUtilities
{
    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) => new Some<T>(value);

        public static Maybe<T> None<T>() => new None<T>();
    }
}
