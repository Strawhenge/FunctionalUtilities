using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class FirstOrNoneTests
    {
        [Fact]
        public void FirstOrNone_GivenSequenceIsEmpty_ShouldReturnNone()
        {
            var subject = Array.Empty<int>();

            var result = subject.FirstOrNone();
            var result2 = subject.FirstOrNone(x => true);
            var result3 = subject.FirstOrNone(x => false);

            AssertMaybe.IsNone(result);
            AssertMaybe.IsNone(result2);
            AssertMaybe.IsNone(result3);
        }

        [Fact]
        public void FirstOrNone_GivenSequenceNotEmpty_ShouldReturnSome()
        {
            var subject = new[] { 10, 1, 2 };

            var result = subject.FirstOrNone();

            AssertMaybe.IsSome(result);
            Assert.Equal(10, result.ReduceUnsafe());
        }

        [Fact]
        public void FirstOrNone_GivenSequenceNotEmpty_AndPredicateMatches_ShouldReturnSome()
        {
            var subject = new[] { 10, 1, 2 };

            var result = subject.FirstOrNone(x => x == 1);

            AssertMaybe.IsSome(result);
            Assert.Equal(1, result.ReduceUnsafe());
        }

        [Fact]
        public void FirstOrNone_GivenSequenceNotEmpty_AndPredicateDoesNotMatche_ShouldReturnNone()
        {
            var subject = new[] { 10, 1, 2 };

            var result = subject.FirstOrNone(x => false);

            AssertMaybe.IsNone(result);
        }
    }
}
