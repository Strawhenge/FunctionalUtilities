namespace FunctionalUtilities
{
    public abstract partial class Maybe<T>
    {
        public static implicit operator Maybe<T>(T value) =>
            Maybe.Some(value);
    }
}
