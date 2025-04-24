using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalUtilities
{
    public static partial class MaybeExtensions
    {
        public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (index < 0) return Maybe.None<T>();

            var array = enumerable.ToArray();

            return index >= array.Length
                ? Maybe.None<T>()
                : Maybe.Some(array[index]);
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> enumerable) =>
            enumerable.FirstOrNone(_ => true);

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var array = enumerable.ToArray();

            return array.Any(predicate)
                ? array.First(predicate)
                : Maybe.None<T>();
        }

        public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> enumerable) =>
            enumerable.SingleOrNone(_ => true);

        public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var array = enumerable.ToArray();

            return array.Any(predicate)
                ? array.Single(predicate)
                : Maybe.None<T>();
        }

        public static IEnumerable<T> WhereSome<T>(this IEnumerable<Maybe<T>> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return Enumerate();

            IEnumerable<T> Enumerate()
            {
                foreach (var maybe in enumerable)
                {
                    if (maybe == null)
                        throw new ArgumentNullException(nameof(enumerable));

                    if (maybe.HasSome(out var value))
                        yield return value;
                }
            }
        }
    }
}