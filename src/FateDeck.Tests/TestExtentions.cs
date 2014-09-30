using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    public static class TestExtentions
    {
        public static T ShouldEqual<T>(this T actual, T expected)
        {
            Assert.AreEqual(expected,actual);
            return actual;
        }
        public static T IsNotNullOrEmpty<T>(this T actual)
        {
            Assert.IsNotNull(actual);
            if (typeof (T) == typeof (string))
                Assert.AreNotEqual(actual, string.Empty);               
            return actual;
        }
    }
}