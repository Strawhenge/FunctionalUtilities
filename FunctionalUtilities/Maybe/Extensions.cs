using System;
using System.Collections.Generic;

namespace FunctionalUtilities
{
    public static partial class MaybeExtensions
    {
        public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct
        {
            if (maybe == null)
                throw new ArgumentNullException(nameof(maybe));

            return maybe
                .Map<T?>(x => x)
                .Reduce(() => null);
        }

        public static Maybe<T> ToMaybe<T>(this T? obj) where T : struct =>
            obj.HasValue
                ? Maybe.Some(obj.Value)
                : Maybe.None<T>();

        public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe)
        {
            if (maybe == null)
                throw new ArgumentNullException(nameof(maybe));

            return maybe.Reduce(Maybe.None<T>);
        }

        public static Maybe<TValue> MaybeGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (!dictionary.TryGetValue(key, out var value))
                return Maybe.None<TValue>();

            if (value == null)
                throw new ArgumentNullException(
                    nameof(dictionary),
                    "The dictionary holds a null value for the key. A Maybe cannot hold null.");

            return Maybe.Some(value);
        }
    }
}