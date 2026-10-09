using System;
using Xunit;

namespace FunctionalUtilities.Tests.EitherTests
{
    public class NullHandlingTests
    {
        [Fact]
        public void MapLeft_should_throw_when_mapping_returns_null()
        {
            var left = Either.Left<string, int>("text");

            var exception = Assert.Throws<ArgumentNullException>(
                () => left.MapLeft<string>(_ => null));

            Assert.Equal("mapping", exception.ParamName);
            Assert.Contains("returned null", exception.Message);
        }

        [Fact]
        public void MapRight_should_throw_when_mapping_returns_null()
        {
            var right = Either.Right<int, string>("text");

            var exception = Assert.Throws<ArgumentNullException>(
                () => right.MapRight<string>(_ => null));

            Assert.Equal("mapping", exception.ParamName);
            Assert.Contains("returned null", exception.Message);
        }

        [Fact]
        public void Implicit_conversion_should_throw_when_left_value_is_null()
        {
            Either<string, int> Convert(string value) => value;

            var exception = Assert.Throws<ArgumentNullException>(
                () => Convert(null));

            Assert.Equal("left", exception.ParamName);
            Assert.Contains("Cannot convert null", exception.Message);
        }

        [Fact]
        public void Implicit_conversion_should_throw_when_right_value_is_null()
        {
            Either<int, string> Convert(string value) => value;

            var exception = Assert.Throws<ArgumentNullException>(
                () => Convert(null));

            Assert.Equal("right", exception.ParamName);
            Assert.Contains("Cannot convert null", exception.Message);
        }

        [Fact]
        public void MapLeft_should_throw_when_mapping_is_null()
        {
            AssertThrowsForLeftAndRight("mapping", either => either.MapLeft<int>(null));
        }

        [Fact]
        public void MapRight_should_throw_when_mapping_is_null()
        {
            AssertThrowsForLeftAndRight("mapping", either => either.MapRight<int>(null));
        }

        [Fact]
        public void DoLeft_should_throw_when_action_is_null()
        {
            AssertThrowsForLeftAndRight("action", either => either.DoLeft(null));
        }

        [Fact]
        public void DoRight_should_throw_when_action_is_null()
        {
            AssertThrowsForLeftAndRight("action", either => either.DoRight(null));
        }

        [Fact]
        public void ReduceLeft_should_throw_when_reducer_is_null()
        {
            AssertThrowsForLeftAndRight("reducer", either => either.ReduceLeft(null));
        }

        [Fact]
        public void ReduceRight_should_throw_when_reducer_is_null()
        {
            AssertThrowsForLeftAndRight("reducer", either => either.ReduceRight(null));
        }

        static void AssertThrowsForLeftAndRight(string paramName, Action<Either<string, string>> act)
        {
            var eithers = new[]
            {
                Either.Left<string, string>("left"),
                Either.Right<string, string>("right")
            };

            foreach (var either in eithers)
            {
                var exception = Assert.Throws<ArgumentNullException>(() => act(either));

                Assert.Equal(paramName, exception.ParamName);
            }
        }
    }
}
