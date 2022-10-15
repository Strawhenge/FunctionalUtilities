using System;

namespace FunctionalUtilities
{
    sealed class Some<T> : Maybe<T>
    {
        readonly T _value;

        public Some(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            _value = value;
        }

        public override void Do(Action<T> action) => action(_value);

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new Some<TNew>(mapping(_value));

        public override T Reduce(Func<T> fallback) => _value;
    }
}