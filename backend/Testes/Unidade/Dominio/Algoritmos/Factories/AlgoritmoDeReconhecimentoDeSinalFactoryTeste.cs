using Aplicacao.Dominio.Algoritmos.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Unidade.Dominio.Algoritmos.Factories
{
    [TestClass]
    public class AlgoritmoDeReconhecimentoDeSinalFactoryTeste
    {
        [TestMethod]
        public void crinando_o_algoritmo_de_reconhecimento_de_sinais_estaticos()
        {
            var factory = new AlgoritmoDeReconhecimentoDeSinalFactory(new GeradorDeCaracteristicasFactory());

            var algoritmo1 = factory.CriarReconhecedorDeSinaisEstaticos();
            var algoritmo2 = factory.CriarReconhecedorDeSinaisEstaticos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }

        [TestMethod]
        public void crinando_o_algoritmo_de_reconhecimento_de_sinais_dinamicos()
        {
            var factory = new AlgoritmoDeReconhecimentoDeSinalFactory(new GeradorDeCaracteristicasFactory());

            var algoritmo1 = factory.CriarReconhecedorDeSinaisDinamicos();
            var algoritmo2 = factory.CriarReconhecedorDeSinaisDinamicos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }

        [TestMethod]
        public void criando_algoritmo_de_reconhecimento_de_frames_de_sinal_dinamico()
        {
            var factory = new AlgoritmoDeReconhecimentoDeSinalFactory(new GeradorDeCaracteristicasFactory());

            var algoritmo1 = factory.CriarReconhecedorDeFramesDeSinaisDinamicos();
            var algoritmo2 = factory.CriarReconhecedorDeFramesDeSinaisDinamicos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }
    }
}