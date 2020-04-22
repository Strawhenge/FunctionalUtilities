using FunctionalUtilities;

public static class EitherExtensions
{
    public static T Reduce<T>(this Either<T, T> either) => either.ReduceLeft(x => x);
}
