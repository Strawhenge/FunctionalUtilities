using System;
using System.Collections.Generic;

namespace FunctionalUtilities
{
    public static partial class MaybeExtensions
    {
        public static Maybe<T> ElementAtOrNone<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (index < 0) return Maybe.None<T>();

            if (enumerable is IList<T> list)
                return index < list.Count
                    ? SomeElement(list[index], nameof(enumerable))
                    : Maybe.None<T>();

            if (enumerable is IReadOnlyList<T> readOnlyList)
                return index < readOnlyList.Count
                    ? SomeElement(readOnlyList[index], nameof(enumerable))
                    : Maybe.None<T>();

            var currentIndex = 0;

            foreach (var element in enumerable)
            {
                if (currentIndex == index)
                    return SomeElement(element, nameof(enumerable));

                currentIndex++;
            }

            return Maybe.None<T>();
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> enumerable) =>
            enumerable.FirstOrNone(_ => true);

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (var element in enumerable)
            {
                if (predicate(element))
                    return SomeElement(element, nameof(enumerable));
            }

            return Maybe.None<T>();
        }

        public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> enumerable) =>
            enumerable.SingleOrNone(_ => true);

        public static Maybe<T> SingleOrNone<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var found = false;
            var match = default(T);

            foreach (var element in enumerable)
            {
                if (!predicate(element))
                    continue;

                if (found)
                    throw new InvalidOperationException("Sequence contains more than one matching element.");

                found = true;
                match = element;
            }

            return found
                ? SomeElement(match, nameof(enumerable))
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

        static Maybe<T> SomeElement<T>(T element, string paramName)
        {
            if (element == null)
                throw new ArgumentNullException(
                    paramName,
                    "The element found in the sequence is null. A Maybe cannot hold null.");

            return Maybe.Some(element);
        }
    }
}