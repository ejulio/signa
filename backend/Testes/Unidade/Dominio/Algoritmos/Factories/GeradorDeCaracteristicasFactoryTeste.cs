using Dominio.Algoritmos.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Unidade.Dominio.Algoritmos.Factories
{
    [TestClass]
    public class GeradorDeCaracteristicasFactoryTeste
    {
        [TestMethod]
        public void criando_um_gerador_de_caracristicas_de_sinal_dinamico()
        {
            var factory = new GeradorDeCaracteristicasFactory();

            var gerador1 = factory.CriarGeradorDeCaracteristicasDeSinalDinamico();
            var gerador2 = factory.CriarGeradorDeCaracteristicasDeSinalDinamico();

            gerador1.Should().BeSameAs(gerador2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }

        [TestMethod]
        public void criando_um_gerador_de_caracristicas_de_sinal_estatico()
        {
            var factory = new GeradorDeCaracteristicasFactory();

            var gerador1 = factory.CriarGeradorDeCaracteristicasDeSinalEstatico();
            var gerador2 = factory.CriarGeradorDeCaracteristicasDeSinalEstatico();

            gerador1.Should().BeSameAs(gerador2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }

        [TestMethod]
        public void criando_um_gerador_de_caracristicas_de_sinal_estatico_com_tipo_de_frame()
        {
            var factory = new GeradorDeCaracteristicasFactory();

            var gerador1 = factory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
            var gerador2 = factory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();

            gerador1.Should().BeSameAs(gerador2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }
    }
}