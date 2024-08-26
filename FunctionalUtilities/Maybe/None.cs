using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalUtilities
{
    sealed class None<T> : Maybe<T>
    {
        internal static None<T> Instance { get; } = new();

        None()
        {
        }

        public override void Do(Action<T> action)
        {
        }

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new None<TNew>();

        public override bool HasSome() => false;

        public override bool HasSome(out T? value)
        {
            value = default;
            return false;
        }

        public override T Reduce(Func<T> fallback) => fallback();
        
        public override IEnumerable<T> AsEnumerable() => Enumerable.Empty<T>();
    }
}