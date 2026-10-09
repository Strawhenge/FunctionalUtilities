using System;
using System.Collections.Generic;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class FirstOrNoneTests
    {
        [Fact]
        public void FirstOrNone_should_return_none_when_sequence_is_empty()
        {
            var subject = Array.Empty<int>();

            var result = subject.FirstOrNone();
            var result2 = subject.FirstOrNone(_ => true);
            var result3 = subject.FirstOrNone(_ => false);

            AssertMaybe.IsNone(result);
            AssertMaybe.IsNone(result2);
            AssertMaybe.IsNone(result3);
        }

        [Fact]
        public void FirstOrNone_should_return_some_when_sequence_is_not_empty()
        {
            var subject = new[] { 10, 1, 2 };

            var result = subject.FirstOrNone();

            AssertMaybe.IsSome(result);
            Assert.Equal(10, (int)result);
        }

        [Fact]
        public void FirstOrNone_should_return_some_when_predicate_matches()
        {
            var subject = new[] { 10, 1, 2 };

            var result = subject.FirstOrNone(x => x == 1);

            AssertMaybe.IsSome(result);
            Assert.Equal(1, (int)result);
        }

        [Fact]
        public void FirstOrNone_should_return_none_when_predicate_does_not_match()
        {
            var subject = new[] { 10, 1, 2 };

            var result = subject.FirstOrNone(_ => false);

            AssertMaybe.IsNone(result);
        }

        [Fact]
        public void FirstOrNone_should_throw_when_first_element_is_null()
        {
            var subject = new[] { null, "second" };

            Assert.Throws<ArgumentNullException>(
                () => subject.FirstOrNone());
        }

        [Fact]
        public void FirstOrNone_should_throw_when_first_element_is_a_null_value_type()
        {
            var subject = new int?[] { null, 1 };

            Assert.Throws<ArgumentNullException>(
                () => subject.FirstOrNone());
        }

        [Fact]
        public void FirstOrNone_should_throw_when_first_matching_element_is_null()
        {
            var subject = new[] { "first", null, "third" };

            Assert.Throws<ArgumentNullException>(
                () => subject.FirstOrNone(x => x != "first"));
        }

        [Fact]
        public void FirstOrNone_should_return_some_when_null_element_is_not_the_first()
        {
            var subject = new[] { "first", null };

            var result = subject.FirstOrNone();

            AssertMaybe.IsSome(result, "first");
        }

        [Fact]
        public void FirstOrNone_should_not_enumerate_past_first_match()
        {
            IEnumerable<int> Sequence()
            {
                yield return 1;
                throw new InvalidOperationException("Enumerated past the first match.");
            }

            var result = Sequence().FirstOrNone();

            AssertMaybe.IsSome(result, 1);
        }

        [Fact]
        public void FirstOrNone_should_evaluate_predicate_once_per_element()
        {
            var subject = new[] { 1, 2, 3 };
            var calls = 0;

            var result = subject.FirstOrNone(x =>
            {
                calls++;
                return x == 2;
            });

            AssertMaybe.IsSome(result, 2);
            Assert.Equal(2, calls);
        }

        [Fact]
        public void FirstOrNone_should_throw_when_predicate_is_null()
        {
            var subject = new[] { 1 };

            Assert.Throws<ArgumentNullException>(
                () => subject.FirstOrNone(null));
        }

        [Fact]
        public void FirstOrNone_should_throw_when_sequence_is_null()
        {
            int[] subject = null;

            Assert.Throws<ArgumentNullException>(
                () => subject.FirstOrNone());

            Assert.Throws<ArgumentNullException>(
                () => subject.FirstOrNone(_ => true));
        }
    }
}
