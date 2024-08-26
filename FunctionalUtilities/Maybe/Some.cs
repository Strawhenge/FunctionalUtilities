using System;
using System.Collections.Generic;

namespace FunctionalUtilities
{
    sealed class Some<T> : Maybe<T>
    {
        readonly T _value;

        internal Some(T value)
        {
            if (value == null) 
                throw new ArgumentNullException(nameof(value));

            _value = value;
        }

        public override void Do(Action<T> action) => action(_value);

        public override Maybe<TNew> Map<TNew>(Func<T, TNew> mapping) => new Some<TNew>(mapping(_value));

        public override Maybe<T> Where(Func<T, bool> predicate) => 
            predicate(_value) ? this : Maybe.None<T>();

        public override bool HasSome() => true;

        public override bool HasSome(out T? value)
        {
            value = _value;
            return true;
        }

        public override T Reduce(Func<T> fallback) => _value;

        public override IEnumerable<T> AsEnumerable()
        {
            yield return _value;
        }
    }
}