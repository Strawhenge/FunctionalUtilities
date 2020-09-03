using System;
using Xunit;

namespace FunctionalUtilities.Tests.UnitTests
{
    public class Right_Tests
    {
        [Fact]
        public void ReduceRight_ShouldReturnExpectedString()
        {
            var expected = "This is the string.";

            var subject = Either.Right<object, string>(expected);

            var actual = subject.ReduceRight(
                _ => throw new Exception("Unexpected reducer call."));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoLeft_ShouldNotInvokeAction()
        {
            bool hasInvoked = false;

            var subject = Either.Right<string, object>(new object());
            subject.DoLeft(_ => hasInvoked = true);

            Assert.False(hasInvoked);
        }

        [Fact]
        public void DoRight_ShouldInvokeAction()
        {
            bool hasInvoked = false;

            var subject = Either.Right<string, object>(new object());
            subject.DoRight(_ => hasInvoked = true);

            Assert.True(hasInvoked);
        }

        [Fact]
        public void ImplicitOperator_ShouldCastRighttTypeToEither()
        {
            Either<object, Exception> either = new Exception();

            AssertEither.Right(either);
        }
    }
}
