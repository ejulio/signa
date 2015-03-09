using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos;

namespace Testes.Unidade.Dominio.Algoritmos
{
    [TestClass]
    public class AlgoritmoDeReconhecimentoDeSinalFactoryTeste
    {
        [TestMethod]
        public void crinando_o_algoritmo_de_reconhecimento_de_sinais_estaticos()
        {
            var factory = new AlgoritmoDeReconhecimentoDeSinalFactory();

            var algoritmo1 = factory.CriarReconhecedorDeSinaisEstaticos();
            var algoritmo2 = factory.CriarReconhecedorDeSinaisEstaticos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma classe, como se fosse um Singleton");
        }

        [TestMethod]
        public void crinando_o_algoritmo_de_reconhecimento_de_sinais_dinamicos()
        {
            var factory = new AlgoritmoDeReconhecimentoDeSinalFactory();

            var algoritmo1 = factory.CriarReconhecedorDeSinaisDinamicos();
            var algoritmo2 = factory.CriarReconhecedorDeSinaisDinamicos();

            algoritmo1.Should().BeSameAs(algoritmo2, "Deveria retornar sempre a mesma classe, como se fosse um Singleton");
        }
    }
}