using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    class IsSomeException : AssertActualExpectedException
    {
        public IsSomeException(object some) : base("None", "Some", "Expected None but was Some")
        {
            Some = some;
        }

        public object Some { get; }
    }
}
