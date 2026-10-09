using System;
using Xunit;

namespace FunctionalUtilities.Tests.EitherTests
{
    public class LeftTests
    {
        [Fact]
        public void ReduceLeft_ShouldReturnExpectedString()
        {
            var expected = "This is the string.";

            var subject = Either.Left<string, object>(expected);

            var actual = subject.ReduceLeft(
                _ => throw new Exception("Unexpected reducer call."));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoLeft_ShouldInvokeAction()
        {
            bool hasInvoked = false;

            var subject = Either.Left<object, string>(new object());
            subject.DoLeft(_ => hasInvoked = true);

            Assert.True(hasInvoked);
        }

        [Fact]
        public void DoRight_ShouldNotInvokeAction()
        {
            bool hasInvoked = false;

            var subject = Either.Left<object, string>(new object());
            subject.DoRight(_ => hasInvoked = true);

            Assert.False(hasInvoked);
        }

        [Fact]
        public void ImplicitOperator_ShouldCastLeftTypeToEither()
        {
            Either<Exception, object> either = new Exception();

            AssertEither.Left(either);
        }

        [Fact]
        public void ReduceRight_ShouldReturnReducerResult()
        {
            var subject = Either.Left<string, int>("abc");

            var actual = subject.ReduceRight(x => x.Length);

            Assert.Equal(3, actual);
        }

        [Fact]
        public void MapLeft_ShouldMapValue()
        {
            var subject = Either.Left<string, object>("abc");

            var result = subject.MapLeft(x => x.Length);

            AssertEither.Left(result);
            Assert.Equal(3, result.ReduceLeft(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void MapRight_ShouldNotInvokeMapping()
        {
            var expected = "This is the string.";

            var subject = Either.Left<string, object>(expected);

            var result = subject.MapRight<int>(_ => throw new Exception("Unexpected mapping call."));

            AssertEither.Left(result);
            Assert.Equal(expected, result.ReduceLeft(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void DoLeft_ShouldReturnSameInstance()
        {
            var subject = Either.Left<object, string>(new object());

            var result = subject.DoLeft(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void DoRight_ShouldReturnSameInstance()
        {
            var subject = Either.Left<object, string>(new object());

            var result = subject.DoRight(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void Left_GivenValueIsNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => Either.Left<string, object>(null));
        }
    }
}
