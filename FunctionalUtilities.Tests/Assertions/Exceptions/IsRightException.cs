using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    class IsRightException : AssertActualExpectedException
    {
        public IsRightException(object right) : base("Left", "Right", "Expected Left but was Right.")
        {
            Right = right;
        }

        public object Right { get; }
    }
}
