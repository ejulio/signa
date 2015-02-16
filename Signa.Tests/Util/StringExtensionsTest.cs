using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Util;

namespace Signa.Tests.Util
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void underscore_a_string()
        {
            var text = "Hello World with  some  SPACES";

            var result = text.Underscore();

            result.Should().Be("Hello_World_with__some__SPACES");
        }
    }
}
