﻿using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class ElementAtOrNoneTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(-100)]
        public void ElementAtOrNone_GivenEnumerableIsEmpty_ShouldReturnNone(int index)
        {
            var subject = Array.Empty<int>();

            var result = subject.ElementAtOrNone(index);
            AssertMaybe.IsNone(result);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(100)]
        [InlineData(-1)]
        [InlineData(-19)]
        public void ElementAtOrNone_GivenIndexIsNotInRange_ShouldReturnNone(int index)
        {
            var subject = new[] { "first", "second", "third" };

            var result = subject.ElementAtOrNone(index);

            AssertMaybe.IsNone(result);
        }

        [Theory]
        [InlineData(0, "first")]
        [InlineData(1, "second")]
        [InlineData(2, "third")]
        public void ElementAtOrNone_GivenIndexIsInRange_ShouldReturnMaybeWithCorrectValue(int index, string expectedResult)
        {
            var subject = new[] { "first", "second", "third" };

            var result = subject.ElementAtOrNone(index);

            AssertMaybe.IsSome(result);

            var reducedResult = (string)result;
            Assert.Equal(expectedResult, reducedResult);
        }
    }
}
