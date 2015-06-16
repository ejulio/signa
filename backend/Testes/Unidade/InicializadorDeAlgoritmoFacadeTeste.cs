using Dominio;
using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;
using Dominio.Algoritmos.Factories;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Testes.Comum.Builders.Dominio.Sinais;
using System.Linq;
using Dominio.Algoritmos.Treinamento;
using Dominio.Persistencia;

namespace Testes.Unidade
{
    [TestClass]
    public class InicializadorDeAlgoritmoFacadeTeste
    {
        private Mock<IAlgoritmoClassificacaoSinalFactory> algoritmoDeReconhecimentoDeSinaisFactory;
        private Mock<IRepositorio<Sinal>> repositorio;
        private Mock<IAlgoritmoClassificacaoSinaisEstaticos> algoritmoDeReconhecimentoDeSinaisEstaticos;
        private Mock<IRepositorioFactory> repositorioFactory;
        private Mock<IAlgoritmoClassificacaoSinaisDinamicos> algoritmoDeReconhecimentoDeSinaisDinamicos;
        private InicializadorDeAlgoritmoFacade inicializadorDeAlgoritmoFacade;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new Mock<IRepositorio<Sinal>>();
            repositorioFactory = new Mock<IRepositorioFactory>();
            algoritmoDeReconhecimentoDeSinaisEstaticos = new Mock<IAlgoritmoClassificacaoSinaisEstaticos>();
            algoritmoDeReconhecimentoDeSinaisDinamicos = new Mock<IAlgoritmoClassificacaoSinaisDinamicos>();
            algoritmoDeReconhecimentoDeSinaisFactory = new Mock<IAlgoritmoClassificacaoSinalFactory>();

            inicializadorDeAlgoritmoFacade =
                new InicializadorDeAlgoritmoFacade(algoritmoDeReconhecimentoDeSinaisFactory.Object, repositorioFactory.Object);

            repositorioFactory
                .Setup(r => r.CriarECarregarRepositorioDeSinaisEstaticos())
                .Returns(repositorio.Object);

            repositorioFactory
                .Setup(r => r.CriarECarregarRepositorioDeSinaisDinamicos())
                .Returns(repositorio.Object);

            algoritmoDeReconhecimentoDeSinaisFactory
                .Setup(f => f.CriarClassificadorSinaisEstaticos())
                .Returns(algoritmoDeReconhecimentoDeSinaisEstaticos.Object);

            algoritmoDeReconhecimentoDeSinaisFactory
                .Setup(f => f.CriarClassificadorFramesSinaisDinamicos())
                .Returns(algoritmoDeReconhecimentoDeSinaisEstaticos.Object);

            algoritmoDeReconhecimentoDeSinaisFactory
                .Setup(f => f.CriarClassificadorSinaisDinamicos())
                .Returns(algoritmoDeReconhecimentoDeSinaisDinamicos.Object);
        }

        [TestMethod]
        public void treinando_o_algoritmo_de_reconhecimento_de_sinais_estaticos()
        {
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(DadaUmaColecaoDeSinaisEstaticos().GetEnumerator);

            inicializadorDeAlgoritmoFacade.TreinarAlgoritmoClassificacaoSinaisEstaticos();

            algoritmoDeReconhecimentoDeSinaisEstaticos
                .Verify(a =>
                    a.Aprender(It.Is<IDadosSinaisEstaticos>(d => VerificarDadosDoAlgoritmoDeSinaisEstaticos(d))));
        }

        [TestMethod]
        public void treinando_o_algoritmo_de_reconhecimento_de_sinais_estaticos_sem_dados()
        {
            var listaDeSinaisVazia = new List<Sinal>();
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(listaDeSinaisVazia.GetEnumerator());

            Action acao = () => inicializadorDeAlgoritmoFacade.TreinarAlgoritmoClassificacaoSinaisEstaticos();

            acao.ShouldNotThrow();
            algoritmoDeReconhecimentoDeSinaisEstaticos
                .Verify(a => a.Aprender(It.IsAny<IDadosSinaisEstaticos>()), Times.Never);
        }

        [TestMethod]
        public void treinando_o_algoritmo_de_reconhecimento_de_sinais_dinamicos()
        {
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(DadaUmaColecaoDeSinaisDinamicos().GetEnumerator);

            inicializadorDeAlgoritmoFacade.TreinarAlgoritmoClassificacaoSinaisDinamicos();

            algoritmoDeReconhecimentoDeSinaisEstaticos
                .Verify(a =>
                    a.Aprender(It.Is<IDadosSinaisEstaticos>(d => VerificarDadosDosLimitesDoAlgoritmoDeSinaisDinamicos(d))));

            algoritmoDeReconhecimentoDeSinaisDinamicos
                .Verify(a =>
                    a.Aprender(It.Is<IDadosSinaisDinamicos>(d => VerificarDadosDoAlgoritmoDeSinaisDinamicos(d))));
        }

        [TestMethod]
        public void treinando_o_algoritmo_de_reconhecimento_de_sinais_dinamicos_sem_dados()
        {
            var listaDeSinaisVazia = new List<Sinal>();
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(listaDeSinaisVazia.GetEnumerator());

            Action acao = () => inicializadorDeAlgoritmoFacade.TreinarAlgoritmoClassificacaoSinaisDinamicos();

            acao.ShouldNotThrow();
            algoritmoDeReconhecimentoDeSinaisEstaticos
                .Verify(a => a.Aprender(It.IsAny<IDadosSinaisEstaticos>()), Times.Never);

            algoritmoDeReconhecimentoDeSinaisDinamicos
                .Verify(a => a.Aprender(It.IsAny<IDadosSinaisDinamicos>()), Times.Never);
        }

        [TestMethod]
        public void treinando_os_algoritmos()
        {
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(DadaUmaColecaoDeSinais().GetEnumerator);

            inicializadorDeAlgoritmoFacade.TreinarAlgoritmosClassificacaoSinais();

            algoritmoDeReconhecimentoDeSinaisEstaticos
                .Verify(a =>
                    a.Aprender(It.IsAny<IDadosSinaisEstaticos>()), Times.Exactly(2));

            algoritmoDeReconhecimentoDeSinaisDinamicos
                .Verify(a =>
                    a.Aprender(It.IsAny<IDadosSinaisDinamicos>()), Times.Once);
        }

        private ICollection<Sinal> DadaUmaColecaoDeSinaisEstaticos()
        {
            var sinais = new ColecaoDeSinaisBuilder()
                .ComQuantidadeDeAmostrasPorSinal(2)
                .ComQuantidadeDeSinais(2)
                .ComGeradorDeAmostrasEstaticas()
                .Construir();

            return sinais;
        }

        private ICollection<Sinal> DadaUmaColecaoDeSinaisDinamicos()
        {
            var sinais = new ColecaoDeSinaisBuilder()
                .ComQuantidadeDeAmostrasPorSinal(2)
                .ComQuantidadeDeSinais(2)
                .ComGeradorDeAmostrasDinamicas()
                .Construir();

            return sinais;
        }

        private ICollection<Sinal> DadaUmaColecaoDeSinais()
        {
            var sinaisDinamicos = new ColecaoDeSinaisBuilder()
                .ComQuantidadeDeAmostrasPorSinal(2)
                .ComQuantidadeDeSinais(2)
                .ComGeradorDeAmostrasDinamicas()
                .Construir();

            var sinaisEstaticos = new ColecaoDeSinaisBuilder()
                .ComQuantidadeDeAmostrasPorSinal(2)
                .ComQuantidadeDeSinais(2)
                .ComGeradorDeAmostrasEstaticas()
                .Construir();

            return sinaisEstaticos
                .Concat(sinaisDinamicos)
                .ToArray();
        }

        private bool VerificarDadosDoAlgoritmoDeSinaisEstaticos(IDadosSinaisEstaticos dados)
        {
            dados.QuantidadeClasses.Should().Be(2);
            dados.IdentificadoresSinais.Should().HaveCount(4);
            dados.CaracteristicasSinais.Should().HaveCount(4);
            return true;
        }

        private bool VerificarDadosDoAlgoritmoDeSinaisDinamicos(IDadosSinaisDinamicos dados)
        {
            dados.QuantidadeClasses.Should().Be(2);
            dados.IdentificadoresSinais.Should().HaveCount(4);
            dados.CaracteristicasSinais.Should().HaveCount(4);
            return true;
        }

        private bool VerificarDadosDosLimitesDoAlgoritmoDeSinaisDinamicos(IDadosSinaisEstaticos dados)
        {
            dados.QuantidadeClasses.Should().Be(4);
            dados.IdentificadoresSinais.Should().HaveCount(12);
            dados.CaracteristicasSinais.Should().HaveCount(12);
            return true;
        }
    }
}
