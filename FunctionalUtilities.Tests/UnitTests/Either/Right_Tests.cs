using System;
using Xunit;

namespace FunctionalUtilities.Tests.UnitTests
{
    public class Right_Tests
    {
        public void ReduceRight_ShouldReturnExpectedString()
        {
            var expected = "This is the string.";

            var subject = Either.Right<object, string>(expected);

            var actual = subject.ReduceRight(
                _ => throw new Exception("Unexpected reducer call."));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
    }
}
