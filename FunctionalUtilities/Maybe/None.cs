using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalUtilities
{
    sealed class None<T> : Maybe<T>
    {
        internal static None<T> Instance { get; } = new None<T>();

        None()
        {
        }

        public override Maybe<T> Do(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            return this;
        }

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping));

            return None<TNew>.Instance;
        }

        public override Maybe<T> Where(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return this;
        }

        public override Maybe<T> Combine(Func<Maybe<T>> combineWith)
        {
            if (combineWith == null)
                throw new ArgumentNullException(nameof(combineWith));

            return combineWith() ?? throw new ArgumentNullException(
                nameof(combineWith),
                "The function given to Combine returned null. Return Maybe.None<T>() when there is no value.");
        }

        public override bool HasSome() => false;

        public override bool HasSome(out T value)
        {
            value = default;
            return false;
        }

        public override bool WhereHas(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return false;
        }

        public override T Reduce(Func<T> fallback)
        {
            if (fallback == null)
                throw new ArgumentNullException(nameof(fallback));

            return fallback();
        }

        public override IEnumerable<T> AsEnumerable() => Enumerable.Empty<T>();
    }
}