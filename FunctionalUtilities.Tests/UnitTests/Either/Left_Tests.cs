﻿using System;
using Xunit;

namespace FunctionalUtilities.Tests.UnitTests
{
    public class Left_Tests
    {
        [Fact]
        public void ReduceRight_ShouldReturnExpectedString()
        {
            var expected = "This is the string.";

            var subject = Either.Left<string, object>(expected);

            var actual = subject.ReduceLeft(
                _ => throw new Exception("Unexpected reducer call."));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitOperator_ShouldCastLeftTypeToEither()
        {
            Either<Exception, object> either = new Exception();

            AssertEither.Left(either);
        }
    }
}
