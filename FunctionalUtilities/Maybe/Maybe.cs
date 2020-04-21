using System;

namespace FunctionalUtilities
{
    public abstract class Maybe<T>
    {
        public abstract void Do(Action<T> action);

        public abstract Maybe<TNew> Map<TNew>(Func<T, TNew> mapping);

        public abstract T Reduce(Func<T> reducer);
    }
}
