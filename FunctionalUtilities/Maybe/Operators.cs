using System;

namespace FunctionalUtilities
{
    public abstract partial class Maybe<T>
    {
        public static implicit operator Maybe<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(
                    nameof(value),
                    "Cannot convert null to a Maybe. Use Maybe.NotNull when the value might be null.");

            return Maybe.Some(value);
        }

        public static explicit operator T(Maybe<T> maybe) => maybe.Reduce(
            () => throw new InvalidCastException("Cannot cast from 'None'."));
    }
}