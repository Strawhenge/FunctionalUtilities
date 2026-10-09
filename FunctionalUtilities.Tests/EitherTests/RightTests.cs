using System;
using Xunit;

namespace FunctionalUtilities.Tests.EitherTests
{
    public class RightTests
    {
        [Fact]
        public void ReduceRight_should_return_value()
        {
            var expected = "This is the string.";

            var subject = Either.Right<object, string>(expected);

            var actual = subject.ReduceRight(
                _ => throw new Exception("Unexpected reducer call."));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoLeft_should_not_invoke_action()
        {
            bool hasInvoked = false;

            var subject = Either.Right<string, object>(new object());
            subject.DoLeft(_ => hasInvoked = true);

            Assert.False(hasInvoked);
        }

        [Fact]
        public void DoRight_should_invoke_action()
        {
            bool hasInvoked = false;

            var subject = Either.Right<string, object>(new object());
            subject.DoRight(_ => hasInvoked = true);

            Assert.True(hasInvoked);
        }

        [Fact]
        public void Implicit_conversion_should_convert_right_type_to_either()
        {
            Either<object, Exception> either = new Exception();

            AssertEither.Right(either);
        }

        [Fact]
        public void ReduceLeft_should_return_reducer_result()
        {
            var subject = Either.Right<int, string>("abc");

            var actual = subject.ReduceLeft(x => x.Length);

            Assert.Equal(3, actual);
        }

        [Fact]
        public void MapRight_should_map_value()
        {
            var subject = Either.Right<object, string>("abc");

            var result = subject.MapRight(x => x.Length);

            AssertEither.Right(result);
            Assert.Equal(3, result.ReduceRight(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void MapLeft_should_not_invoke_mapping()
        {
            var expected = "This is the string.";

            var subject = Either.Right<object, string>(expected);

            var result = subject.MapLeft<int>(_ => throw new Exception("Unexpected mapping call."));

            AssertEither.Right(result);
            Assert.Equal(expected, result.ReduceRight(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void DoLeft_should_return_same_instance()
        {
            var subject = Either.Right<string, object>(new object());

            var result = subject.DoLeft(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void DoRight_should_return_same_instance()
        {
            var subject = Either.Right<string, object>(new object());

            var result = subject.DoRight(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void Right_should_throw_when_value_is_null()
        {
            Assert.Throws<ArgumentNullException>(
                () => Either.Right<object, string>(null));
        }
    }
}
