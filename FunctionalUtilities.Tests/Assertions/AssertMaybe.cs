﻿using Xunit;

namespace FunctionalUtilities.Tests
{
    public static class AssertMaybe
    {
        public static void IsSome<T>(Maybe<T> maybe)
        {
            Assert.NotNull(maybe);
            maybe.Reduce(() => throw new IsNoneException());
        }

        public static void IsNone<T>(Maybe<T> maybe)
        {
            Assert.NotNull(maybe);
            maybe.Do(x => throw new IsSomeException(x));
        }
    }
}
