using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class SingleOrNoneTests
    {
        [Fact]
        public void SingleOrNone_should_return_none_when_sequence_is_empty()
        {
            var subject = Array.Empty<int>();

            var result = subject.SingleOrNone();
            var result2 = subject.SingleOrNone(_ => true);
            var result3 = subject.SingleOrNone(_ => false);

            AssertMaybe.IsNone(result);
            AssertMaybe.IsNone(result2);
            AssertMaybe.IsNone(result3);
        }

        [Fact]
        public void SingleOrNone_should_return_some_when_sequence_has_one_element()
        {
            var subject = new[] { 10 };

            var result = subject.SingleOrNone();

            AssertMaybe.IsSome(result);
            Assert.Equal(10, (int)result);
        }

        [Fact]
        public void SingleOrNone_should_throw_when_sequence_has_more_than_one_element()
        {
            var subject = new[] { 1, 0 };

            Assert.Throws<InvalidOperationException>(
                () => subject.SingleOrNone());
        }

        [Fact]
        public void SingleOrNone_should_return_none_when_predicate_does_not_match()
        {
            var subject = new[] { 1, 0 };

            var result = subject.SingleOrNone(_ => false);

            AssertMaybe.IsNone(result);
        }

        [Fact]
        public void SingleOrNone_should_return_some_when_predicate_matches_one_element()
        {
            var subject = new[] { 1, 6, 3 };

            var result = subject.SingleOrNone(x => x == 6);

            AssertMaybe.IsSome(result);
            Assert.Equal(6, (int)result);
        }

        [Fact]
        public void SingleOrNone_should_throw_when_predicate_matches_more_than_one_element()
        {
            var subject = new[] { 1, 1, 0 };

            Assert.Throws<InvalidOperationException>(
                () => subject.SingleOrNone(x => x == 1));
        }

        [Fact]
        public void SingleOrNone_should_throw_when_single_element_is_null()
        {
            var subject = new string[] { null };

            Assert.Throws<ArgumentNullException>(
                () => subject.SingleOrNone());
        }

        [Fact]
        public void SingleOrNone_should_throw_when_single_matching_element_is_null()
        {
            var subject = new[] { "first", null };

            Assert.Throws<ArgumentNullException>(
                () => subject.SingleOrNone(x => x != "first"));
        }

        [Fact]
        public void SingleOrNone_should_evaluate_predicate_once_per_element()
        {
            var subject = new[] { 1, 2, 3 };
            var calls = 0;

            var result = subject.SingleOrNone(x =>
            {
                calls++;
                return x == 2;
            });

            AssertMaybe.IsSome(result, 2);
            Assert.Equal(3, calls);
        }

        [Fact]
        public void SingleOrNone_should_throw_when_predicate_is_null()
        {
            var subject = new[] { 1 };

            Assert.Throws<ArgumentNullException>(
                () => subject.SingleOrNone(null));
        }

        [Fact]
        public void SingleOrNone_should_throw_when_sequence_is_null()
        {
            int[] subject = null;

            Assert.Throws<ArgumentNullException>(
                () => subject.SingleOrNone());

            Assert.Throws<ArgumentNullException>(
                () => subject.SingleOrNone(_ => true));
        }
    }
}
