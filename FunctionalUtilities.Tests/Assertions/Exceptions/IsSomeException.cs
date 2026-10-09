using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    class IsSomeException : XunitException
    {
        public IsSomeException(object some) : base("Expected None but was Some")
        {
            Some = some;
        }

        public object Some { get; }
    }
}
