using System;
using System.Linq;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class WhereSomeTests
    {
        [Fact]
        public void WhereSome_ShouldReturnEmpty_WhenEmpty()
        {
            var maybeStrings = Array.Empty<Maybe<string>>();

            var strings = maybeStrings
                .WhereSome()
                .ToArray();

            Assert.Empty(strings);
        }

        [Fact]
        public void WhereSome_ShouldReturnEmpty_WhenOnlyContainsNone()
        {
            var maybeStrings = new[]
            {
                Maybe.None<string>(),
                Maybe.None<string>(),
                Maybe.None<string>()
            };

            var strings = maybeStrings
                .WhereSome()
                .ToArray();

            Assert.Empty(strings);
        }

        [Fact]
        public void WhereSome_ShouldReturnAllValues_WhenContainsSomeAndNone()
        {
            const string hammer = "hammer";
            const string screwdriver = "screwdriver";
            const string saw = "saw";

            var maybeStrings = new[]
            {
                Maybe.Some(hammer),
                Maybe.None<string>(),
                Maybe.None<string>(),
                Maybe.Some(screwdriver),
                Maybe.None<string>(),
                Maybe.Some(saw),
            };

            var strings = maybeStrings
                .WhereSome()
                .ToArray();

            Assert.Equal(3, strings.Length);

            Assert.Equal(hammer, strings[0]);
            Assert.Equal(screwdriver, strings[1]);
            Assert.Equal(saw, strings[2]);
        }
    }
}