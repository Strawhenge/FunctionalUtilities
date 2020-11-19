using FunctionalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

public static partial class MaybeExtensions
{
    public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> enumerable, int index)
    {
        if (index < 0) return Maybe.None<T>();

        var count = enumerable.Count();

        return index >= count
            ? Maybe.None<T>()
            : Maybe.Some(enumerable.ElementAt(index));
    }

    public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> enumerable) =>
        enumerable.FirstOrNone(x => true);

    public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        if (!enumerable.Any(predicate)) return Maybe.None<T>();

        var value = enumerable.First(predicate);

        return value == null
            ? Maybe.None<T>()
            : Maybe.Some(value);
    }

    public static IEnumerable<T> AsEnumerable<T>(this Maybe<T> maybe)
    {
        return maybe
            .Map(x => GetEnumerable(x))
            .Reduce(() => Enumerable.Empty<T>());

        IEnumerable<T> GetEnumerable(T value)
        {
            yield return value;
        }
    }
}
