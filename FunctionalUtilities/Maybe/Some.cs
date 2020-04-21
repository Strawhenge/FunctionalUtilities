using System;

namespace FunctionalUtilities
{
    internal sealed class Some<T> : Maybe<T>
    {
        private readonly T value;

        public Some(T value)
        {
            this.value = value;
        }

        public override void Do(Action<T> action) => action(value);

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new Some<TNew>(mapping(value));

        public override T Reduce(Func<T> fallback) => value;
    }
}
