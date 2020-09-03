using System;
using Xunit;

namespace FunctionalUtilities.Tests.UnitTests
{
    public class Maybe_Tests
    {
        [Fact]
        public void Reduce_GivenSomeString_ShouldReturnString()
        {
            var expected = "This is the string.";

            var subject = Maybe.Some(expected);

            var actual = subject.Reduce(
                reducer: () => throw new Exception("Unexpected call to reducer"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Reduce_GivenNoneString_ShouldReturnReducerResult()
        {
            var expected = "This is the string.";

            var subject = Maybe.None<string>();

            var actual = subject.Reduce(reducer: () => expected);

            Assert.Equal(expected, actual);
        }
    }
}
