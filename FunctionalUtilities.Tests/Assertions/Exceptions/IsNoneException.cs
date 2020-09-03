using Xunit.Sdk;

namespace FunctionalUtilities.Tests
{
    internal class IsNoneException : AssertActualExpectedException
    {
        public IsNoneException() : base("Some", "None", "Expected Some but was None")
        {
        }
    }
}
