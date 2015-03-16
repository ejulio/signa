using Dominio.Util;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Unidade.Util
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void colocando_underscore_em_uma_string()
        {
            const string texto = "Hello World with  some  SPACES";

            var resultado = texto.Underscore();

            resultado.Should().Be("Hello_World_with__some__SPACES");
        }

        [TestMethod]
        public void removendo_acentos_de_uma_string()
        {
            const string texto = "duas mãos com INFORMAÇÕES do sinal em um frequência";

            var resultado = texto.RemoverAcentos();
            resultado.Should().Be("duas maos com INFORMACOES do sinal em um frequencia");
        }
    }
}
