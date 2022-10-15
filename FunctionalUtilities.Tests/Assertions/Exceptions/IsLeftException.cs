using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    class IsLeftException : AssertActualExpectedException
    {
        public IsLeftException(object left) : base("Right", "Left", "Expected Right but was Left.")
        {
            Left = left;
        }

        public object Left { get; }
    }
}
