using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunctionalUtilities.Tests
{
    // These mirror the examples in README.md so that they are compiled and run.
    // When one changes, change the other.
    public class ReadmeExamplesTests
    {
        class User
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
            public bool IsAdmin { get; set; }
        }

        readonly Dictionary<int, User> _users = new Dictionary<int, User>
        {
            [42] = new User { Name = "Ada", Email = null, IsActive = true, IsAdmin = true }
        };

        Maybe<User> FindUser(int id) => _users.MaybeGetValue(id);

        static Either<string, int> Parse(string text)
        {
            if (int.TryParse(text, out var number))
                return number;

            return $"'{text}' is not a number";
        }

        [Fact]
        public void Creating_a_maybe()
        {
            string text = null;

            Maybe<string> some = Maybe.Some("Ada");
            Maybe<string> none = Maybe.None<string>();
            Maybe<string> converted = "Ada";

            Maybe<string> notNull = Maybe.NotNull(text);
            Maybe<string> notEmpty = Maybe.NotNullOrEmpty(text);
            Maybe<string> notBlank = Maybe.NotNullOrWhiteSpace(text);

            int? nullable = 5;
            Maybe<int> fromNullable = nullable.ToMaybe();

            AssertMaybe.IsSome(some, "Ada");
            AssertMaybe.IsNone(none);
            AssertMaybe.IsSome(converted, "Ada");
            AssertMaybe.IsNone(notNull);
            AssertMaybe.IsNone(notEmpty);
            AssertMaybe.IsNone(notBlank);
            AssertMaybe.IsSome(fromNullable, 5);
            Assert.Throws<ArgumentNullException>(() => Maybe.Some<string>(null));
        }

        [Fact]
        public void Transforming_and_unwrapping_a_maybe()
        {
            string greeting = FindUser(42)
                .Where(user => user.IsActive)
                .Map(user => user.Name)
                .Map(name => $"Hello, {name}")
                .Reduce(() => "Hello, guest");

            string guestGreeting = FindUser(7)
                .Where(user => user.IsActive)
                .Map(user => user.Name)
                .Map(name => $"Hello, {name}")
                .Reduce(() => "Hello, guest");

            Assert.Equal("Hello, Ada", greeting);
            Assert.Equal("Hello, guest", guestGreeting);
        }

        [Fact]
        public void Mapping_to_a_value_that_might_be_null()
        {
            Maybe<string> email = FindUser(42)
                .Map(user => Maybe.NotNull(user.Email))
                .Flatten();

            AssertMaybe.IsNone(email);
        }

        [Fact]
        public void Running_an_action_on_a_maybe()
        {
            var names = new List<string>();

            FindUser(42).Do(user => names.Add(user.Name));
            FindUser(7).Do(user => names.Add(user.Name));

            Assert.Equal(new[] { "Ada" }, names.ToArray());
        }

        [Fact]
        public void Checking_a_maybe_for_a_value()
        {
            Maybe<User> maybe = FindUser(42);

            var name = "";
            if (maybe.HasSome(out var user))
                name = user.Name;

            bool hasUser = maybe.HasSome();
            bool isAdmin = maybe.WhereHas(u => u.IsAdmin);

            User cast = (User)maybe;
            int? asNullable = Maybe.Some(5).ToNullable();

            Assert.Equal("Ada", name);
            Assert.True(hasUser);
            Assert.True(isAdmin);
            Assert.Equal("Ada", cast.Name);
            Assert.Equal(5, asNullable);
            Assert.False(FindUser(7).WhereHas(u => u.IsAdmin));
            Assert.Throws<InvalidCastException>(() => (User)FindUser(7));
        }

        [Fact]
        public void Falling_back_to_another_maybe()
        {
            Maybe<User> user = FindUser(7).Combine(() => FindUser(42));

            AssertMaybe.IsSome(user);
        }

        [Fact]
        public void Using_maybe_with_collections()
        {
            var numbers = new[] { 3, 8, 12 };

            Maybe<int> first = numbers.FirstOrNone();
            Maybe<int> firstBig = numbers.FirstOrNone(n => n > 5);
            Maybe<int> onlyHuge = numbers.SingleOrNone(n => n > 10);
            Maybe<int> third = numbers.ElementAtOrNone(2);
            Maybe<int> missing = numbers.ElementAtOrNone(10);

            IEnumerable<int> values =
                new[] { Maybe.Some(1), Maybe.None<int>(), Maybe.Some(3) }.WhereSome();

            IEnumerable<int> zeroOrOne = first.AsEnumerable();

            AssertMaybe.IsSome(first, 3);
            AssertMaybe.IsSome(firstBig, 8);
            AssertMaybe.IsSome(onlyHuge, 12);
            AssertMaybe.IsSome(third, 12);
            AssertMaybe.IsNone(missing);
            Assert.Equal(new[] { 1, 3 }, values.ToArray());
            Assert.Equal(new[] { 3 }, zeroOrOne.ToArray());
            Assert.Throws<InvalidOperationException>(() => numbers.SingleOrNone(n => n > 5));
        }

        [Fact]
        public void Creating_an_either()
        {
            Either<string, int> left = Either.Left<string, int>("failed");
            Either<string, int> right = Either.Right<string, int>(1);

            AssertEither.Left(left);
            AssertEither.Right(right);
            AssertEither.Right(Parse("21"));
            AssertEither.Left(Parse("abc"));
        }

        [Fact]
        public void Using_an_either()
        {
            string success = Parse("21")
                .MapRight(number => number * 2)
                .MapRight(number => $"Result: {number}")
                .ReduceRight(error => $"Error: {error}");

            string failure = Parse("abc")
                .MapRight(number => $"Result: {number}")
                .MapLeft(error => $"Error: {error}")
                .Reduce();

            Assert.Equal("Result: 42", success);
            Assert.Equal("Error: 'abc' is not a number", failure);
        }

        [Fact]
        public void Running_an_action_on_an_either()
        {
            var log = new List<string>();
            var total = 0;

            foreach (var input in new[] { "21", "abc", "4" })
            {
                Parse(input)
                    .DoLeft(error => log.Add(error))
                    .DoRight(number => total += number);
            }

            Assert.Equal(new[] { "'abc' is not a number" }, log.ToArray());
            Assert.Equal(25, total);
        }
    }
}
