using System;

namespace FunctionalUtilities
{
    public abstract partial class Maybe<T>
    {
        public static implicit operator Maybe<T>(T value) => Maybe.Some(value);

        public static explicit operator T(Maybe<T> maybe) => maybe.Reduce(
            () => throw new InvalidCastException("Cannot cast from 'None'."));
    }
}