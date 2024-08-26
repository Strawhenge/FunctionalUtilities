using System;

namespace FunctionalUtilities
{
    sealed class None<T> : Maybe<T>
    {
        internal static None<T> Instance { get; } = new None<T>();
        
        None()
        {
        }

        public override void Do(Action<T> action)
        {
        }

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new None<TNew>();

        public override T Reduce(Func<T> fallback) => fallback();
    }
}