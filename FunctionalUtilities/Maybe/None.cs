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

        public override Maybe<T> Do(Action<T> action) => this;

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new None<TNew>();

        public override Maybe<T> Where(Func<T, bool> predicate) => this;

        public override Maybe<T> Combine(Func<Maybe<T>> combineWith) => combineWith();

        public override bool HasSome() => false;

        public override bool HasSome(out T value)
        {
            value = default;
            return false;
        }

        public override bool WhereHas(Func<T, bool> predicate) => false;

        public override T Reduce(Func<T> fallback) => fallback();

        public override IEnumerable<T> AsEnumerable() => Enumerable.Empty<T>();
    }
}