using FunctionalUtilities;

public static class MaybeExtensions
{
    public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct => maybe
        .Map<T?>(x => x)
        .Reduce(() => null);

    public static Maybe<T> ToMaybe<T>(this T? obj) where T : struct => obj.HasValue
        ? Maybe.Some(obj.Value)
        : Maybe.None<T>();

    public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe) => maybe.Reduce(() => Maybe.None<T>());
}
