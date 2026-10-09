# FunctionalUtilities

Two small functional types for C#: `Maybe<T>`, a value that may be absent, and `Either<TLeft, TRight>`, a value that is one of two types.

The library targets .NET Standard 2.0, has no dependencies, and works in Unity 2020.3 and later.

## Install

### Unity

In the Package Manager, choose **Add package from git URL** and enter:

```
https://github.com/Strawhenge/FunctionalUtilities.git?path=/Package#release
```

### NuGet

The package is published to GitHub Packages as `FunctionalUtilities`. The package source is:

```
https://nuget.pkg.github.com/Strawhenge/index.json
```

GitHub Packages needs a personal access token even for public packages. See [Working with the NuGet registry](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry) for how to add the source, then:

```
dotnet add package FunctionalUtilities
```

Everything is in the `FunctionalUtilities` namespace.

## Maybe

A `Maybe<T>` is either `Some`, holding a value, or `None`. It never holds `null`.

### Creating

```csharp
Maybe<string> some = Maybe.Some("Ada");
Maybe<string> none = Maybe.None<string>();
Maybe<string> converted = "Ada";                            // implicit conversion

Maybe<string> notNull = Maybe.NotNull(text);                // None when text is null
Maybe<string> notEmpty = Maybe.NotNullOrEmpty(text);
Maybe<string> notBlank = Maybe.NotNullOrWhiteSpace(text);

int? nullable = 5;
Maybe<int> fromNullable = nullable.ToMaybe();
```

`Maybe.Some(null)` throws `ArgumentNullException`. Use `Maybe.NotNull` when the value might be null.

### Transforming and unwrapping

`Map`, `Where` and `Do` do nothing on a `None`, so calls can be chained without checking. `Reduce` takes the value out, with a fallback for `None`.

```csharp
Maybe<User> FindUser(int id) => _users.MaybeGetValue(id);

string greeting = FindUser(42)
    .Where(user => user.IsActive)
    .Map(user => user.Name)
    .Map(name => $"Hello, {name}")
    .Reduce(() => "Hello, guest");
```

The function given to `Map` must not return null. When it might, map to a `Maybe` and flatten:

```csharp
Maybe<string> email = FindUser(42)
    .Map(user => Maybe.NotNull(user.Email))
    .Flatten();
```

`Do` runs an action on the value and returns the same `Maybe`:

```csharp
FindUser(42).Do(user => Console.WriteLine(user.Name));
```

### Checking for a value

```csharp
Maybe<User> maybe = FindUser(42);

if (maybe.HasSome(out var user))
    Console.WriteLine(user.Name);

bool hasUser = maybe.HasSome();
bool isAdmin = maybe.WhereHas(u => u.IsAdmin);     // false for None

User cast = (User)maybe;                           // throws InvalidCastException for None
int? asNullable = Maybe.Some(5).ToNullable();
```

### Falling back to another Maybe

`Combine` returns the original when it is `Some`, and otherwise the result of the function.

```csharp
Maybe<User> user = FindInCache(id).Combine(() => FindInDatabase(id));
```

### Collections

```csharp
var numbers = new[] { 3, 8, 12 };

Maybe<int> first = numbers.FirstOrNone();                  // Some(3)
Maybe<int> firstBig = numbers.FirstOrNone(n => n > 5);     // Some(8)
Maybe<int> onlyHuge = numbers.SingleOrNone(n => n > 10);   // Some(12)
Maybe<int> third = numbers.ElementAtOrNone(2);             // Some(12)
Maybe<int> missing = numbers.ElementAtOrNone(10);          // None

IEnumerable<int> values =
    new[] { Maybe.Some(1), Maybe.None<int>(), Maybe.Some(3) }.WhereSome();   // 1, 3

IEnumerable<int> zeroOrOne = first.AsEnumerable();         // 3
```

`SingleOrNone` throws `InvalidOperationException` when more than one element matches.

`MaybeGetValue` looks up a dictionary key and returns `None` when the key is missing.

If any of these finds an element that is `null`, it throws `ArgumentNullException`. A null there usually means a validation or mapping error upstream, so it is not hidden as `None`.

## Either

An `Either<TLeft, TRight>` holds a left value or a right value. By convention the left is the failure and the right is the success.

### Creating

```csharp
Either<string, int> left = Either.Left<string, int>("failed");
Either<string, int> right = Either.Right<string, int>(1);
```

Both types convert implicitly, which keeps methods short:

```csharp
Either<string, int> Parse(string text)
{
    if (int.TryParse(text, out var number))
        return number;

    return $"'{text}' is not a number";
}
```

Neither side can be null.

### Using

`MapLeft` and `MapRight` transform one side and leave the other alone. `ReduceRight` returns the right value, converting a left with the function given; `ReduceLeft` is the mirror.

```csharp
string success = Parse("21")
    .MapRight(number => number * 2)
    .MapRight(number => $"Result: {number}")
    .ReduceRight(error => $"Error: {error}");       // "Result: 42"
```

When both sides have the same type, `Reduce` returns whichever is present:

```csharp
string failure = Parse("abc")
    .MapRight(number => $"Result: {number}")
    .MapLeft(error => $"Error: {error}")
    .Reduce();                                      // "Error: 'abc' is not a number"
```

`DoLeft` and `DoRight` run an action on one side and return the same `Either`:

```csharp
Parse(input)
    .DoLeft(error => log.Add(error))
    .DoRight(number => total += number);
```

## Building

Building needs the .NET 10 SDK and Windows, because the library's post-build step uses `xcopy`.

```
dotnet build
dotnet test
```

## License

MIT
