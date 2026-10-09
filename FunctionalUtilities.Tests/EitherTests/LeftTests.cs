using System;
using Xunit;

namespace FunctionalUtilities.Tests.EitherTests
{
    public class LeftTests
    {
        [Fact]
        public void ReduceLeft_should_return_value()
        {
            var expected = "This is the string.";

            var subject = Either.Left<string, object>(expected);

            var actual = subject.ReduceLeft(
                _ => throw new Exception("Unexpected reducer call."));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DoLeft_should_invoke_action()
        {
            bool hasInvoked = false;

            var subject = Either.Left<object, string>(new object());
            subject.DoLeft(_ => hasInvoked = true);

            Assert.True(hasInvoked);
        }

        [Fact]
        public void DoRight_should_not_invoke_action()
        {
            bool hasInvoked = false;

            var subject = Either.Left<object, string>(new object());
            subject.DoRight(_ => hasInvoked = true);

            Assert.False(hasInvoked);
        }

        [Fact]
        public void Implicit_conversion_should_convert_left_type_to_either()
        {
            Either<Exception, object> either = new Exception();

            AssertEither.Left(either);
        }

        [Fact]
        public void ReduceRight_should_return_reducer_result()
        {
            var subject = Either.Left<string, int>("abc");

            var actual = subject.ReduceRight(x => x.Length);

            Assert.Equal(3, actual);
        }

        [Fact]
        public void MapLeft_should_map_value()
        {
            var subject = Either.Left<string, object>("abc");

            var result = subject.MapLeft(x => x.Length);

            AssertEither.Left(result);
            Assert.Equal(3, result.ReduceLeft(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void MapRight_should_not_invoke_mapping()
        {
            var expected = "This is the string.";

            var subject = Either.Left<string, object>(expected);

            var result = subject.MapRight<int>(_ => throw new Exception("Unexpected mapping call."));

            AssertEither.Left(result);
            Assert.Equal(expected, result.ReduceLeft(_ => throw new Exception("Unexpected reducer call.")));
        }

        [Fact]
        public void DoLeft_should_return_same_instance()
        {
            var subject = Either.Left<object, string>(new object());

            var result = subject.DoLeft(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void DoRight_should_return_same_instance()
        {
            var subject = Either.Left<object, string>(new object());

            var result = subject.DoRight(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void Left_should_throw_when_value_is_null()
        {
            Assert.Throws<ArgumentNullException>(
                () => Either.Left<string, object>(null));
        }
    }
}
