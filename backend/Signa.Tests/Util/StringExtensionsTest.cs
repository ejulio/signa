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

        [TestMethod]
        public void removing_accents_from_a_string()
        {
            var text = "duas mãos com INFORMAÇÕES do sinal em um frequência";

            var result = text.RemoveAccents();
            result.Should().Be("duas maos com INFORMACOES do sinal em um frequencia");
        }
    }
}
