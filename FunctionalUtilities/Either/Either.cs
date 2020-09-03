using System;

namespace FunctionalUtilities
{
    public abstract partial class Either<TLeft, TRight>
    {
        public abstract Either<TNewLeft, TRight> MapLeft<TNewLeft>(Func<TLeft, TNewLeft> mapping);

        public abstract Either<TLeft, TNewRight> MapRight<TNewRight>(Func<TRight, TNewRight> mapping);

        public abstract void DoLeft(Action<TLeft> action);

        public abstract void DoRight(Action<TRight> action);

        public abstract TLeft ReduceLeft(Func<TRight, TLeft> reducer);

        public abstract TRight ReduceRight(Func<TLeft, TRight> reducer);
    }
}
