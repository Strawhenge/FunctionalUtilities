using System;
using System.Collections.Generic;

namespace FunctionalUtilities
{
    public abstract partial class Maybe<T>
    {
        public abstract Maybe<T> Do(Action<T> action);

        public abstract Maybe<TNew> Map<TNew>(Func<T, TNew> mapping);

        public abstract Maybe<T> Where(Func<T, bool> predicate);

        public abstract bool HasSome();
        
        public abstract bool HasSome(out T value);
        
        public abstract T Reduce(Func<T> reducer);

        public abstract IEnumerable<T> AsEnumerable();
    }
}
