using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class SomeTests
    {
        [Fact]
        public void Reduce_should_return_value()
        {
            var expected = "This is the string.";

            var subject = Maybe.Some(expected);

            var actual = subject.Reduce(
                fallback: () => throw new Exception("Unexpected call to reducer"));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_should_invoke_action()
        {
            bool hasInvoked = false;

            var subject = Maybe.Some(new object());

            subject.Do(_ => hasInvoked = true);

            Assert.True(hasInvoked);
        }

        [Fact]
        public void Map_then_reduce_should_return_mapping_result()
        {
            var expected = new object();

            var subject = Maybe.Some("This is a string.");

            var actual = subject
                .Map(_ => expected)
                .Reduce(fallback: () => throw new Exception("Unexpected call to reducer"));

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_should_return_same_instance()
        {
            var subject = Maybe.Some(new object());

            var result = subject.Do(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void Where_should_return_same_instance_when_predicate_is_true()
        {
            var subject = Maybe.Some("This is a string.");

            var result = subject.Where(x => x.Length > 0);

            Assert.Same(subject, result);
        }

        [Fact]
        public void Where_should_return_none_when_predicate_is_false()
        {
            var subject = Maybe.Some("This is a string.");

            var result = subject.Where(x => x.Length == 0);

            AssertMaybe.IsNone(result);
        }

        [Fact]
        public void HasSome_should_return_true()
        {
            var subject = Maybe.Some("This is a string.");

            Assert.True(subject.HasSome());
        }

        [Fact]
        public void HasSome_should_return_true_and_value()
        {
            var expected = "This is a string.";

            var subject = Maybe.Some(expected);

            Assert.True(subject.HasSome(out var actual));
            Assert.Equal(expected, actual);
        }
    }
}