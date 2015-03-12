using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Dados.Repositorio;
using Signa.Dominio;
using Signa.Dominio.Algoritmos;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio
{
    [TestClass]
    public class InicializadorDeAlgoritmoFacadeTeste
    {
        private Mock<IAlgoritmoDeReconhecimentoDeSinalFactory> algoritmoDeReconhecimentoDeSinaisFactory;
        private Mock<IRepositorio<Sinal>> repositorio;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos> algoritmoDeReconhecimentoDeSinaisEstaticos;
        private Mock<IRepositorioFactory> repositorioFactory;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisDinamicos> algoritmoDeReconhecimentoDeSinaisDinamicos;
        private InicializadorDeAlgoritmoFacade inicializadorDeAlgoritmoFacade;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new Mock<IRepositorio<Sinal>>();
            repositorioFactory = new Mock<IRepositorioFactory>();
            algoritmoDeReconhecimentoDeSinaisEstaticos = new Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos>();
            algoritmoDeReconhecimentoDeSinaisDinamicos = new Mock<IAlgoritmoDeReconhecimentoDeSinaisDinamicos>();
            algoritmoDeReconhecimentoDeSinaisFactory = new Mock<IAlgoritmoDeReconhecimentoDeSinalFactory>();

            inicializadorDeAlgoritmoFacade =
                new InicializadorDeAlgoritmoFacade(algoritmoDeReconhecimentoDeSinaisFactory.Object, repositorioFactory.Object);

            repositorioFactory
                .Setup(r => r.CriarECarregarRepositorioDeSinaisEstaticos())
                .Returns(repositorio.Object);

            repositorioFactory
                .Setup(r => r.CriarECarregarRepositorioDeSinaisDinamicos())
                .Returns(repositorio.Object);

            algoritmoDeReconhecimentoDeSinaisFactory
                .Setup(f => f.CriarReconhecedorDeSinaisEstaticos())
                .Returns(algoritmoDeReconhecimentoDeSinaisEstaticos.Object);

            algoritmoDeReconhecimentoDeSinaisFactory
                .Setup(f => f.CriarReconhecedorDeFramesDeSinaisDinamicos())
                .Returns(algoritmoDeReconhecimentoDeSinaisEstaticos.Object);

            algoritmoDeReconhecimentoDeSinaisFactory
                .Setup(f => f.CriarReconhecedorDeSinaisDinamicos())
                .Returns(algoritmoDeReconhecimentoDeSinaisDinamicos.Object);
        }

        [TestMethod]
        public void treinando_o_algoritmo_de_reconhecimento_de_sinais_estaticos()
        {
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(DadaUmaColecaoDeSinaisEstaticos().GetEnumerator);

            inicializadorDeAlgoritmoFacade.TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();

            algoritmoDeReconhecimentoDeSinaisEstaticos
                .Verify(a =>
                    a.Treinar(It.Is<IGeradorDeDadosDeSinaisEstaticos>(d => VerificarDadosDoAlgoritmoDeSinaisEstaticos(d))));
        }

        [TestMethod]
        public void treinando_o_algoritmo_de_reconhecimento_de_sinais_estaticos_sem_dados()
        {
            var listaDeSinaisVazia = new List<Sinal>();
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(listaDeSinaisVazia.GetEnumerator());

            Action acao = () => inicializadorDeAlgoritmoFacade.TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();

            acao.ShouldNotThrow();
        }

        [TestMethod]
        public void treinando_o_algoritmo_de_reconhecimento_de_sinais_dinamicos()
        {
            repositorio
                .Setup(r => r.GetEnumerator())
                .Returns(DadaUmaColecaoDeSinaisDinamicos().GetEnumerator);

            inicializadorDeAlgoritmoFacade.TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos();

            algoritmoDeReconhecimentoDeSinaisEstaticos
                .Verify(a =>
                    a.Treinar(It.Is<IGeradorDeDadosDeSinaisEstaticos>(d => VerificarDadosDosLimitesDoAlgoritmoDeSinaisDinamicos(d))));

            algoritmoDeReconhecimentoDeSinaisDinamicos
                .Verify(a =>
                    a.Treinar(It.Is<IGeradorDeDadosDeSinaisDinamicos>(d => VerificarDadosDoAlgoritmoDeSinaisDinamicos(d))));
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

        private bool VerificarDadosDoAlgoritmoDeSinaisEstaticos(IGeradorDeDadosDeSinaisEstaticos dados)
        {
            dados.QuantidadeDeClasses.Should().Be(2);
            dados.Saidas.Should().HaveCount(4);
            dados.Entradas.Should().HaveCount(4);
            return true;
        }

        private bool VerificarDadosDoAlgoritmoDeSinaisDinamicos(IGeradorDeDadosDeSinaisDinamicos geradorDeDados)
        {
            geradorDeDados.QuantidadeDeClasses.Should().Be(2);
            geradorDeDados.Saidas.Should().HaveCount(4);
            geradorDeDados.Entradas.Should().HaveCount(4);
            return true;
        }

        private bool VerificarDadosDosLimitesDoAlgoritmoDeSinaisDinamicos(IGeradorDeDadosDeSinaisEstaticos dados)
        {
            dados.QuantidadeDeClasses.Should().Be(2);
            dados.Saidas.Should().HaveCount(8);
            dados.Entradas.Should().HaveCount(8);
            return true;
        }
    }
}
