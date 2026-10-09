using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    class IsRightException : XunitException
    {
        public IsRightException(object right) : base("Expected Left but was Right.")
        {
            Right = right;
        }

        public object Right { get; }
    }
}
