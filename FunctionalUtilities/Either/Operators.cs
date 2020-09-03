namespace FunctionalUtilities
{
    public abstract partial class Either<TLeft, TRight>
    {
        public static implicit operator Either<TLeft, TRight>(TLeft left) =>
            Either.Left<TLeft, TRight>(left);

        public static implicit operator Either<TLeft, TRight>(TRight right) =>
            Either.Right<TLeft, TRight>(right);
    }
}
