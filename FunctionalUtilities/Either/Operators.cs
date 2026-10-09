using System;

namespace FunctionalUtilities
{
    public abstract partial class Either<TLeft, TRight>
    {
        public static implicit operator Either<TLeft, TRight>(TLeft left)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left), "Cannot convert null to an Either.");

            return Either.Left<TLeft, TRight>(left);
        }

        public static implicit operator Either<TLeft, TRight>(TRight right)
        {
            if (right == null)
                throw new ArgumentNullException(nameof(right), "Cannot convert null to an Either.");

            return Either.Right<TLeft, TRight>(right);
        }
    }
}
