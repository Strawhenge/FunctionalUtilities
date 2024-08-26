using System;
using FunctionalUtilities;

public static class EitherExtensions
{
    public static T Reduce<T>(this Either<T, T> either)
    {
        if (either == null)
            throw new ArgumentNullException(nameof(either));

        return either.ReduceLeft(x => x);
    }
}