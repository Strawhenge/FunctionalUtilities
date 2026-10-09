# FunctionalUtilities

A small C# library providing `Maybe<T>` and `Either<TLeft, TRight>`. It ships as a NuGet package on GitHub Packages and as a Unity package.

## Layout

- `FunctionalUtilities/` is the library (`netstandard2.0`).
- `FunctionalUtilities.Tests/` holds the xUnit v3 tests (`net10.0`).
- `Package/` is the Unity package wrapper. `Package/Runtime/FunctionalUtilities.dll` is build output and is gitignored; the `.meta` files beside it are tracked because Unity needs them.

## Build and test

- Requires the .NET 10 SDK.
- `dotnet build`
- `dotnet test` runs on Microsoft Testing Platform, which `global.json` turns on.
- The library's post-build step uses `xcopy`, so the solution builds on Windows only.

## Constraints

- The library stays on `netstandard2.0` with no package dependencies. Unity 2020.3 loads the compiled DLL.
- No `LangVersion` is set, so library code compiles as C# 7.3.
- Don't change or remove existing public members. Ask before any breaking change.

## Conventions

- `Some`, `None`, `Left` and `Right` are internal. Callers create values through the `Maybe` and `Either` factory classes or the implicit conversions.
- `Some`, `Left` and `Right` throw `ArgumentNullException` for a null value.
- Extension methods on reference types throw `ArgumentNullException` when their `this` argument is null.
- Every method that takes a function throws `ArgumentNullException` naming that parameter when the function is null, whether or not it would have been called.
- When a function must not return null and does (`Map`, `MapLeft`, `MapRight`, `Combine`), the method throws `ArgumentNullException` naming the function parameter, with a message saying it returned null.
- Tests live in one class per feature under `MaybeTests/` or `EitherTests/`, and use the `AssertMaybe` and `AssertEither` helpers. Two naming styles exist (`Method_GivenX_ShouldY` and `Sentence_in_snake_case`); match the file being edited.

## Working on changes

- Work on a branch and open a pull request into `master`. Keep commits small.
- Run `dotnet test` before committing.
- For a bug fix, write the failing test first.

## Releases

- The `Release` workflow is started manually. It publishes NuGet package version `1.2.x` and rebuilds the `release` branch that Unity projects pull from.
- Never start a release or push to `release` unless asked.
