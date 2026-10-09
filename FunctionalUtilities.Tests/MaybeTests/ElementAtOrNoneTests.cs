using System;
using System.Collections;
using System.Collections.Generic;
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

        [Fact]
        public void ElementAtOrNone_GivenElementAtIndexIsNull_ShouldThrow()
        {
            var subject = new[] { "first", null, "third" };

            Assert.Throws<ArgumentNullException>(
                () => subject.ElementAtOrNone(1));
        }

        [Fact]
        public void ElementAtOrNone_ShouldNotEnumeratePastIndex()
        {
            IEnumerable<string> Sequence()
            {
                yield return "first";
                yield return "second";
                throw new InvalidOperationException("Enumerated past the index.");
            }

            var result = Sequence().ElementAtOrNone(1);

            AssertMaybe.IsSome(result, "second");
        }

        [Fact]
        public void ElementAtOrNone_GivenLazySequence_AndIndexIsNotInRange_ShouldReturnNone()
        {
            IEnumerable<string> Sequence()
            {
                yield return "first";
                yield return "second";
            }

            var result = Sequence().ElementAtOrNone(2);

            AssertMaybe.IsNone(result);
        }

        [Fact]
        public void ElementAtOrNone_GivenSequenceIsNull_ShouldThrow()
        {
            int[] subject = null;

            Assert.Throws<ArgumentNullException>(
                () => subject.ElementAtOrNone(0));
        }

        [Fact]
        public void ElementAtOrNone_GivenReadOnlyList_AndIndexIsInRange_ShouldReturnSomeWithoutEnumerating()
        {
            var subject = new ReadOnlyListOnly<string>("first", "second", "third");

            var result = subject.ElementAtOrNone(1);

            AssertMaybe.IsSome(result, "second");
        }

        [Fact]
        public void ElementAtOrNone_GivenReadOnlyList_AndIndexIsNotInRange_ShouldReturnNone()
        {
            var subject = new ReadOnlyListOnly<string>("first", "second", "third");

            var result = subject.ElementAtOrNone(3);

            AssertMaybe.IsNone(result);
        }

        // Implements IReadOnlyList<T> but not IList<T>, and refuses to be enumerated.
        class ReadOnlyListOnly<T> : IReadOnlyList<T>
        {
            readonly T[] _items;

            public ReadOnlyListOnly(params T[] items)
            {
                _items = items;
            }

            public T this[int index] => _items[index];

            public int Count => _items.Length;

            public IEnumerator<T> GetEnumerator() =>
                throw new InvalidOperationException("Expected indexing, not enumeration.");

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
