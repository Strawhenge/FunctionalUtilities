using System;

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
    }
}