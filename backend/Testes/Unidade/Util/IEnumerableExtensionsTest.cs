using Dominio.Util;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Testes.Unidade.Util
{
    [TestClass]
    public class IEnumerableExtensionsTest
    {
        [TestMethod]
        public void concatenando_arrays_de_double()
        {
            var arrays = new[]
            {
                new [] { 0.8, 0.7, 0.5 },
                new [] { 1.4, 1.6, 1.8 }
            };

            var arrayConcatenado = arrays.Concatenar().ToArray();

            var arrayEsperado = new[] { 0.8, 0.7, 0.5, 1.4, 1.6, 1.8 };

            arrayConcatenado.Should().HaveSameCount(arrayEsperado);
            arrayConcatenado.Should().ContainInOrder(arrayEsperado);
        }
    }
}