using System;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class NoneTests
    {
        [Fact]
        public void Reduce_should_return_fallback_result()
        {
            var expected = "This is the string.";

            var subject = Maybe.None<string>();

            var actual = subject.Reduce(fallback: () => expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_should_not_invoke_action()
        {
            bool hasInvoked = false;

            var subject = Maybe.None<object>();

            subject.Do(_ => hasInvoked = true);

            Assert.False(hasInvoked);
        }

        [Fact]
        public void Map_then_reduce_should_return_fallback_result()
        {
            var expected = new object();

            var subject = Maybe.None<string>();

            var actual = subject
                .Map<object>(_ => throw new Exception("Unexpected mapping call."))
                .Reduce(fallback: () => expected);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Map_should_return_shared_none_instance()
        {
            var subject = Maybe.None<string>();

            var result = subject.Map(x => x.Length);

            Assert.Same(Maybe.None<int>(), result);
        }

        [Fact]
        public void Do_should_return_same_instance()
        {
            var subject = Maybe.None<object>();

            var result = subject.Do(_ => { });

            Assert.Same(subject, result);
        }

        [Fact]
        public void Where_should_return_none_without_invoking_predicate()
        {
            bool hasInvoked = false;

            var subject = Maybe.None<string>();

            var result = subject.Where(_ =>
            {
                hasInvoked = true;
                return true;
            });

            AssertMaybe.IsNone(result);
            Assert.False(hasInvoked);
        }

        [Fact]
        public void HasSome_should_return_false()
        {
            var subject = Maybe.None<string>();

            Assert.False(subject.HasSome());
        }

        [Fact]
        public void HasSome_should_return_false_and_default_value()
        {
            var subject = Maybe.None<string>();

            Assert.False(subject.HasSome(out var actual));
            Assert.Null(actual);
        }
    }
}
