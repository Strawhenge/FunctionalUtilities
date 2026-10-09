using System;

namespace FunctionalUtilities
{
    sealed class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        readonly TRight _value;

        public Right(TRight value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            _value = value;
        }

        public override Either<TLeft, TRight> DoLeft(Action<TLeft> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            return this;
        }

        public override Either<TLeft, TRight> DoRight(Action<TRight> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            action(_value);
            return this;
        }

        public override Either<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping));

            return new Right<TNewLeft, TRight>(_value);
        }

        public override Either<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping));

            var result = mapping(_value);

            if (result == null)
                throw new ArgumentNullException(
                    nameof(mapping),
                    "The mapping function returned null. An Either cannot hold null.");

            return new Right<TLeft, TNewRight>(result);
        }

        public override TLeft ReduceLeft(Func<TRight, TLeft> reducer)
        {
            if (reducer == null)
                throw new ArgumentNullException(nameof(reducer));

            return reducer(_value);
        }

        public override TRight ReduceRight(Func<TLeft, TRight> reducer)
        {
            if (reducer == null)
                throw new ArgumentNullException(nameof(reducer));

            return _value;
        }
    }
}