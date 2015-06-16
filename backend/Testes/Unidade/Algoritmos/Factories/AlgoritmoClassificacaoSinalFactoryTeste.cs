using Dominio.Algoritmos.Factories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Unidade.Algoritmos.Factories
{
    [TestClass]
    public class AlgoritmoClassificacaoSinalFactoryTeste
    {
        [TestMethod]
        public void crinando_o_algoritmo_de_reconhecimento_de_sinais_estaticos()
        {
            var factory = new AlgoritmoClassificacaoSinalFactory(new CaracteristicasFactory());

            var algoritmo1 = factory.CriarClassificadorSinaisEstaticos();
            var algoritmo2 = factory.CriarClassificadorSinaisEstaticos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }

        [TestMethod]
        public void crinando_o_algoritmo_de_reconhecimento_de_sinais_dinamicos()
        {
            var factory = new AlgoritmoClassificacaoSinalFactory(new CaracteristicasFactory());

            var algoritmo1 = factory.CriarClassificadorSinaisDinamicos();
            var algoritmo2 = factory.CriarClassificadorSinaisDinamicos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }

        [TestMethod]
        public void criando_algoritmo_de_reconhecimento_de_frames_de_sinal_dinamico()
        {
            var factory = new AlgoritmoClassificacaoSinalFactory(new CaracteristicasFactory());

            var algoritmo1 = factory.CriarClassificadorFramesSinaisDinamicos();
            var algoritmo2 = factory.CriarClassificadorFramesSinaisDinamicos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma instância, como se fosse um Singleton");
        }
    }
}