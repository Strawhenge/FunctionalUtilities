using System;

namespace FunctionalUtilities
{
    internal sealed class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        private readonly TLeft value;

        public Left(TLeft value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            this.value = value;
        }

        public override Either<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping) =>
            new Left<TNewLeft, TRight>(mapping(value));

        public override Either<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping) =>
            new Left<TLeft, TNewRight>(value);

        public override TLeft ReduceLeft(Func<TRight, TLeft> mapping) => value;

        public override TRight ReduceRight(Func<TLeft, TRight> mapping) => mapping(value);
    }
}
