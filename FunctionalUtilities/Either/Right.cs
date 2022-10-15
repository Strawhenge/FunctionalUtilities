﻿using System;

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

        public override void DoLeft(Action<TLeft> action)
        {
        }

        public override void DoRight(Action<TRight> action) => action(_value);

        public override Either<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping) =>
            new Right<TNewLeft, TRight>(_value);

        public override Either<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping) =>
            new Right<TLeft, TNewRight>(mapping(_value));

        public override TLeft ReduceLeft(Func<TRight, TLeft> mapping) => mapping(_value);

        public override TRight ReduceRight(Func<TLeft, TRight> mapping) => _value;
    }
}