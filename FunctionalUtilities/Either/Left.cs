using System;

namespace FunctionalUtilities
{
    sealed class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        readonly TLeft _value;

        public Left(TLeft value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            _value = value;
        }

        public override Either<TLeft, TRight> DoLeft(Action<TLeft> action)
        {
            action(_value);
            return this;
        }

        public override Either<TLeft, TRight> DoRight(Action<TRight> action) => this;

        public override Either<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping) =>
            new Left<TNewLeft, TRight>(mapping(_value));

        public override Either<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping) =>
            new Left<TLeft, TNewRight>(_value);

        public override TLeft ReduceLeft(Func<TRight, TLeft> mapping) => _value;

        public override TRight ReduceRight(Func<TLeft, TRight> mapping) => mapping(_value);
    }
}
