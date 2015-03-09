﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dados
{
    [TestClass]
    public class SinaisEstaticosControllerTest
    {
        private Mock<IRepositorio<Sinal>> repositorio;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos> algoritmo;
        private SinaisEstaticosController sinaisEstaticosController;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new Mock<IRepositorio<Sinal>>();
            algoritmo = new Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos>();
            sinaisEstaticosController = new SinaisEstaticosController(repositorio.Object, algoritmo.Object);
        }

        [TestMethod]
        public void adicionando_um_sinal_que_nao_existe()
        {
            const string descricaoDoSinal = "new sign";
            DadoQueORepositorioRetorneNullParaADescricao(descricaoDoSinal);

            var sinal = DadoUmSinalComDuasAmostras(descricaoDoSinal);

            sinaisEstaticosController.Adicionar(sinal);

            DeveTerAdicionadoOSinalNoRepositorioESalvoAsAlteracoes(sinal);
        }

        [TestMethod]
        public void adicionando_um_sinal_que_ja_existe()
        {
            const string descricaoDoSinal = "old sign";
            var sinalAntigo = DadoQueORepositorioRetorneUmSinalParaADescricao(descricaoDoSinal);

            var novoSinal = DadoUmSinalComDuasAmostras(descricaoDoSinal);

            sinaisEstaticosController.Adicionar(novoSinal);

            DeveTerJuntadoAsAmostrasESalvoAsAlteracoes(sinalAntigo, novoSinal);
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int idDoSinal = 23;
            var amostra = new AmostraBuilder().ConstruirAmostraEstatica();
            algoritmo.Setup(a => a.Reconhecer(amostra)).Returns(idDoSinal);

            var sinalReconhecido = sinaisEstaticosController.Reconhecer(amostra);

            sinalReconhecido.Should().Be(idDoSinal);
        }

        private Sinal DadoQueORepositorioRetorneUmSinalParaADescricao(string descricaoDoSinal)
        {
            var sinal = DadoUmSinalComDuasAmostras(descricaoDoSinal);

            repositorio
                .Setup(r => r.BuscarPorDescricao(descricaoDoSinal))
                .Returns(sinal);

            return sinal;
        }

        private void DadoQueORepositorioRetorneNullParaADescricao(string descricaoDoSinal)
        {
            repositorio.Setup(r => r.BuscarPorDescricao(descricaoDoSinal)).Returns((Sinal)null);
        }

        private Sinal DadoUmSinalComDuasAmostras(string descricaoDoSinal)
        {
            var sinal = new SinalBuilder()
                            .ComDescricao(descricaoDoSinal)
                            .ComAmostra(new AmostraBuilder().Construir())
                            .ComAmostra(new AmostraBuilder().Construir())
                            .Construir();
            return sinal;
        }

        private void DeveTerAdicionadoOSinalNoRepositorioESalvoAsAlteracoes(Sinal sinalEstatico)
        {
            repositorio.Verify(r => r.Adicionar(sinalEstatico));
            repositorio.Verify(r => r.SalvarAlteracoes());
        }

        private void DeveTerJuntadoAsAmostrasESalvoAsAlteracoes(Sinal sinalAntigo, Sinal novoSinal)
        {
            repositorio.Verify(r => r.Adicionar(It.IsAny<Sinal>()), Times.Never);
            repositorio.Verify(r => r.SalvarAlteracoes());
            sinalAntigo.Amostras.Count.Should().Be(4);
            sinalAntigo.Amostras.Should().Contain(novoSinal.Amostras);
        }
    }
}