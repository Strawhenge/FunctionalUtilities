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

            Assert.Throws<ArgumentNullException>(
                () => left.MapLeft<string>(_ => null));
        }

        [Fact]
        public void MapRight_should_throw_when_mapping_returns_null()
        {
            var right = Either.Right<int, string>("text");

            Assert.Throws<ArgumentNullException>(
                () => right.MapRight<string>(_ => null));
        }

        [Fact]
        public void Implicit_conversion_should_throw_when_left_value_is_null()
        {
            Either<string, int> Convert(string value) => value;

            Assert.Throws<ArgumentNullException>(
                () => Convert(null));
        }

        [Fact]
        public void Implicit_conversion_should_throw_when_right_value_is_null()
        {
            Either<int, string> Convert(string value) => value;

            Assert.Throws<ArgumentNullException>(
                () => Convert(null));
        }

        [Fact]
        public void MapLeft_should_throw_when_mapping_is_null()
        {
            AssertThrowsForLeftAndRight(either => either.MapLeft<int>(null));
        }

        [Fact]
        public void MapRight_should_throw_when_mapping_is_null()
        {
            AssertThrowsForLeftAndRight(either => either.MapRight<int>(null));
        }

        [Fact]
        public void DoLeft_should_throw_when_action_is_null()
        {
            AssertThrowsForLeftAndRight(either => either.DoLeft(null));
        }

        [Fact]
        public void DoRight_should_throw_when_action_is_null()
        {
            AssertThrowsForLeftAndRight(either => either.DoRight(null));
        }

        [Fact]
        public void ReduceLeft_should_throw_when_reducer_is_null()
        {
            AssertThrowsForLeftAndRight(either => either.ReduceLeft(null));
        }

        [Fact]
        public void ReduceRight_should_throw_when_reducer_is_null()
        {
            AssertThrowsForLeftAndRight(either => either.ReduceRight(null));
        }

        static void AssertThrowsForLeftAndRight(Action<Either<string, string>> act)
        {
            var eithers = new[]
            {
                Either.Left<string, string>("left"),
                Either.Right<string, string>("right")
            };

            foreach (var either in eithers)
            {
                Assert.Throws<ArgumentNullException>(() => act(either));
            }
        }
    }
}
