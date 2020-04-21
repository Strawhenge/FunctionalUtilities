using System;

namespace FunctionalUtilities
{
    internal sealed class None<T> : Maybe<T>
    {
        public override void Do(Action<T> action)
        {
        }

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new None<TNew>();

        public override T Reduce(Func<T> fallback) => fallback();
    }
}
