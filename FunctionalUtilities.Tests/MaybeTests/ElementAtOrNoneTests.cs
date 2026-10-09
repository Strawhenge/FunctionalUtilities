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
        public void ElementAtOrNone_should_return_none_when_sequence_is_empty(int index)
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
        public void ElementAtOrNone_should_return_none_when_index_is_not_in_range(int index)
        {
            var subject = new[] { "first", "second", "third" };

            var result = subject.ElementAtOrNone(index);

            AssertMaybe.IsNone(result);
        }

        [Theory]
        [InlineData(0, "first")]
        [InlineData(1, "second")]
        [InlineData(2, "third")]
        public void ElementAtOrNone_should_return_some_when_index_is_in_range(int index, string expectedResult)
        {
            var subject = new[] { "first", "second", "third" };

            var result = subject.ElementAtOrNone(index);

            AssertMaybe.IsSome(result);

            var reducedResult = (string)result;
            Assert.Equal(expectedResult, reducedResult);
        }

        [Fact]
        public void ElementAtOrNone_should_throw_when_element_at_index_is_null()
        {
            var subject = new[] { "first", null, "third" };

            Assert.Throws<ArgumentNullException>(
                () => subject.ElementAtOrNone(1));
        }

        [Fact]
        public void ElementAtOrNone_should_not_enumerate_past_index()
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
        public void ElementAtOrNone_should_return_none_when_index_is_not_in_range_of_lazy_sequence()
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
        public void ElementAtOrNone_should_throw_when_sequence_is_null()
        {
            int[] subject = null;

            Assert.Throws<ArgumentNullException>(
                () => subject.ElementAtOrNone(0));
        }

        [Fact]
        public void ElementAtOrNone_should_return_some_without_enumerating_when_index_is_in_range_of_read_only_list()
        {
            var subject = new ReadOnlyListOnly<string>("first", "second", "third");

            var result = subject.ElementAtOrNone(1);

            AssertMaybe.IsSome(result, "second");
        }

        [Fact]
        public void ElementAtOrNone_should_return_none_when_index_is_not_in_range_of_read_only_list()
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
