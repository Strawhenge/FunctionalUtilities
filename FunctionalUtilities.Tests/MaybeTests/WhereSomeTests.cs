using System;
using System.Linq;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class WhereSomeTests
    {
        [Fact]
        public void WhereSome_should_return_empty_when_sequence_is_empty()
        {
            var maybeStrings = Array.Empty<Maybe<string>>();

            var strings = maybeStrings
                .WhereSome()
                .ToArray();

            Assert.Empty(strings);
        }

        [Fact]
        public void WhereSome_should_return_empty_when_sequence_only_contains_none()
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
        public void WhereSome_should_return_all_values_when_sequence_contains_some_and_none()
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

        [Fact]
        public void WhereSome_should_throw_when_sequence_is_null()
        {
            Maybe<string>[] maybeStrings = null;

            Assert.Throws<ArgumentNullException>(
                () => maybeStrings.WhereSome());
        }

        [Fact]
        public void WhereSome_should_throw_when_sequence_contains_null()
        {
            var maybeStrings = new[]
            {
                Maybe.Some("hammer"),
                null
            };

            Assert.Throws<ArgumentNullException>(
                () => maybeStrings.WhereSome().ToArray());
        }
    }
}