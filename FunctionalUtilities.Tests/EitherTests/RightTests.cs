using System;
using Xunit;

namespace FunctionalUtilities.Tests.EitherTests
{
    public class RightTests
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

        [Fact]
        public void ReduceLeft_ShouldReturnReducerResult()
        {
            var subject = Either.Right<int, string>("abc");

            var actual = subject.ReduceLeft(x => x.Length);

            Assert.Equal(3, actual);
        }

        [Fact]
        public void MapRight_ShouldMapValue()
        {
            var subject = Either.Right<object, string>("abc");

            var result = subject.MapRight(x => x.Length);

            AssertEither.Right(result);
            Assert.Equal(3, result.ReduceRight(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void MapLeft_ShouldNotInvokeMapping()
        {
            var expected = "This is the string.";

            var subject = Either.Right<object, string>(expected);

            var result = subject.MapLeft<int>(_ => throw new Exception("Unexpected mapping call."));

            AssertEither.Right(result);
            Assert.Equal(expected, result.ReduceRight(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void DoLeft_ShouldReturnSameInstance()
        {
            var subject = Either.Right<string, object>(new object());

            var result = subject.DoLeft(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void DoRight_ShouldReturnSameInstance()
        {
            var subject = Either.Right<string, object>(new object());

            var result = subject.DoRight(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void Right_GivenValueIsNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => Either.Right<object, string>(null));
        }
    }
}
