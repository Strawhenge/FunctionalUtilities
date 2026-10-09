using System;
using Xunit;

namespace FunctionalUtilities.Tests.EitherTests
{
    public class ReduceTests
    {
        [Fact]
        public void Left_should_return_left_value()
        {
            var either = Either.Left<string, string>("left");

            Assert.Equal("left", either.Reduce());
        }

        [Fact]
        public void Right_should_return_right_value()
        {
            var either = Either.Right<string, string>("right");

            Assert.Equal("right", either.Reduce());
        }

        [Fact]
        public void Null_either_should_throw()
        {
            Either<string, string> either = null;

            Assert.Throws<ArgumentNullException>(
                () => either.Reduce());
        }
    }
}
