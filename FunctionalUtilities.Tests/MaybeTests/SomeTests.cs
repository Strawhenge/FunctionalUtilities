using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class SomeTests
    {
        [Fact]
        public void Reduce_ShouldReturnExpected()
        {
            var expected = "This is the string.";

            var subject = Maybe.Some(expected);

            var actual = subject.Reduce(
                reducer: () => throw new Exception("Unexpected call to reducer"));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_ShouldInvokeAction()
        {
            bool hasInvoked = false;

            var subject = Maybe.Some(new object());

            subject.Do(_ => hasInvoked = true);

            Assert.True(hasInvoked);
        }

        [Fact]
        public void Map_ThenReduce_ShouldReturnMappingResult()
        {
            var expected = new object();

            var subject = Maybe.Some("This is a string.");

            var actual = subject
                .Map(_ => expected)
                .Reduce(reducer: () => throw new Exception("Unexpected call to reducer"));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
    }
}