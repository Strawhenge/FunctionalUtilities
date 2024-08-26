using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class SingleOrNoneTests
    {
        [Fact]
        public void SingleOrNone_GivenSequenceIsEmpty_ShouldReturnNone()
        {
            var subject = Array.Empty<int>();

            var result = subject.SingleOrNone();
            var result2 = subject.SingleOrNone(x => true);
            var result3 = subject.SingleOrNone(x => false);

            AssertMaybe.IsNone(result);
            AssertMaybe.IsNone(result2);
            AssertMaybe.IsNone(result3);
        }

        [Fact]
        public void SingleOrNone_GivenSequenceHasOneItem_ShouldReturnSome()
        {
            var subject = new[] { 10 };

            var result = subject.SingleOrNone();

            AssertMaybe.IsSome(result);
            Assert.Equal(10, (int)result);
        }

        [Fact]
        public void SingleOrNone_GivenSequenceHasMoreThanOneElement_ShouldThrow()
        {
            var subject = new[] { 1, 0 };

            Assert.Throws<InvalidOperationException>(
                () => subject.SingleOrNone());
        }

        [Fact]
        public void SingleOrNone_GivenSequenceHasMoreThanOneElement_AndPredicateDoesNotMatch_ShouldReturnNone()
        {
            var subject = new[] { 1, 0 };

            var result = subject.SingleOrNone(x => false);

            AssertMaybe.IsNone(result);
        }

        [Fact]
        public void SingleOrNone_GivenPredicateMatchesOneElement_ShouldReturnSome()
        {
            var subject = new[] { 1, 6, 3 };

            var result = subject.SingleOrNone(x => x == 6);

            AssertMaybe.IsSome(result);
            Assert.Equal(6, (int)result);
        }

        [Fact]
        public void SingleOrNone_GivenPredicateMatchesMoreThanOneElement_ShouldThrow()
        {
            var subject = new[] { 1, 1, 0 };

            Assert.Throws<InvalidOperationException>(
                () => subject.SingleOrNone(x => x == 1));
        }
    }
}
