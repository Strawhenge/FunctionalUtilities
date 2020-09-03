using System;

namespace FunctionalUtilities
{
    internal sealed class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        private readonly TRight value;

        public Right(TRight value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            this.value = value;
        }

        public override void DoLeft(Action<TLeft> action)
        {
        }

        public override void DoRight(Action<TRight> action) => action(value);

        public override Either<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping) =>
            new Right<TNewLeft, TRight>(value);

        public override Either<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping) =>
            new Right<TLeft, TNewRight>(mapping(value));

        public override TLeft ReduceLeft(Func<TRight, TLeft> mapping) => mapping(value);

        public override TRight ReduceRight(Func<TLeft, TRight> mapping) => value;
    }
}
