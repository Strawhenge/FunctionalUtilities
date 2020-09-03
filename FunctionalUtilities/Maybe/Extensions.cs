using FunctionalUtilities;
using System;

public static class MaybeExtensions
{
    public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct => maybe
        .Map<T?>(x => x)
        .Reduce(() => null);

    public static Maybe<T> ToMaybe<T>(this T? obj) where T : struct => obj.HasValue
        ? Maybe.Some(obj.Value)
        : Maybe.None<T>();

    public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe) => maybe.Reduce(() => Maybe.None<T>());

    public static bool HasValue<T>(this Maybe<T> maybe) => maybe
        .Map(_ => true)
        .Reduce(() => false);

    public static T ReduceUnsafe<T>(this Maybe<T> maybe) => maybe.Reduce(
        () => throw new InvalidOperationException($"Cannot reduce value from {maybe.GetType().Name}"));
}
