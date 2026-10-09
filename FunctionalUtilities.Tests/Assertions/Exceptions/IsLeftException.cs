using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    class IsLeftException : XunitException
    {
        public IsLeftException(object left) : base("Expected Right but was Left.")
        {
            Left = left;
        }

        public object Left { get; }
    }
}
