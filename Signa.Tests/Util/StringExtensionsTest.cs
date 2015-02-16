using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Util;

namespace Signa.Tests.Util
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void hyphenate_a_string()
        {
            var text = "Hello World with  some  SPACES";

            var result = text.Hyphenate();

            result.Should().Be("Hello-World-with--some--SPACES");
        }
    }
}
