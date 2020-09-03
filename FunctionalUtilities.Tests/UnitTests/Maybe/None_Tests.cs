using System;
using Xunit;

namespace FunctionalUtilities.Tests.UnitTests
{
    public class None_Tests
    {
        [Fact]
        public void Reduce_ShouldReturnReducerResult()
        {
            var expected = "This is the string.";

            var subject = Maybe.None<string>();

            var actual = subject.Reduce(reducer: () => expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_ShouldNotInvokeAction()
        {
            bool hasInvoked = false;

            var subject = Maybe.None<object>();

            subject.Do(_ => hasInvoked = true);

            Assert.False(hasInvoked);
        }

        [Fact]
        public void Map_ThenReduce_ShouldReturnReducerResult()
        {
            var expected = new object();

            var subject = Maybe.None<string>();

            var actual = subject
                .Map<object>(_ => throw new Exception("Unexpected mapping call."))
                .Reduce(reducer: () => expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
    }
}
