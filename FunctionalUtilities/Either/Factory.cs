namespace FunctionalUtilities
{
    public static class Either
    {
        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value) => new Left<TLeft, TRight>(value);

        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value) => new Right<TLeft, TRight>(value);
    }
}
