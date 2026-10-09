using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    class IsNoneException : XunitException
    {
        public IsNoneException() : base("Expected Some but was None")
        {
        }
    }
}
