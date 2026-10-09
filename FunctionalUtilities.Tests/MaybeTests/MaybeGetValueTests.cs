using System;
using System.Collections.Generic;
using Xunit;

namespace FunctionalUtilities.Tests.MaybeTests
{
    public class MaybeGetValueTests
    {
        [Fact]
        public void Missing_key_should_return_none()
        {
            var dictionary = new Dictionary<string, string>();

            AssertMaybe.IsNone(dictionary.MaybeGetValue("key"));
        }

        [Fact]
        public void Existing_key_should_return_some_with_value()
        {
            var dictionary = new Dictionary<string, string> { ["key"] = "value" };

            AssertMaybe.IsSome(dictionary.MaybeGetValue("key"), "value");
        }

        [Fact]
        public void Existing_key_with_null_value_should_throw()
        {
            var dictionary = new Dictionary<string, string> { ["key"] = null };

            var exception = Assert.Throws<ArgumentNullException>(
                () => dictionary.MaybeGetValue("key"));

            Assert.Equal("dictionary", exception.ParamName);
        }
    }
}
