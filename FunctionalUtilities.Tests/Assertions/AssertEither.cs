namespace FunctionalUtilities.Tests
{
    public static class AssertEither
    {
        public static void Left<TLeft, TRight>(Either<TLeft, TRight> either) =>
            either.ReduceLeft(
                reducer: x => throw new IsRightException(x));

        public static void Right<TLeft, TRight>(Either<TLeft, TRight> either) =>
            either.ReduceRight(
                reducer: x => throw new IsLeftException(x));
    }
}
