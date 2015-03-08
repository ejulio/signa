using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Domain.Algoritmos;
using Signa.Domain.Sinais.Estatico;
using Signa.Tests.Common.Builders.Dominio.Sinais.Estatico;

namespace Signa.Tests.Dados
{
    [TestClass]
    public class SinaisEstaticosControllerTest
    {
        private Mock<IRepositorio<SinalEstatico>> repositorio;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos> algoritmo;
        private SinaisEstaticosController sinaisEstaticosController;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new Mock<IRepositorio<SinalEstatico>>();
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
            var amostra = new AmostraBuilder().Construir();
            algoritmo.Setup(a => a.Reconhecer(amostra)).Returns(idDoSinal);

            var sinalReconhecido = sinaisEstaticosController.Reconhecer(amostra);

            sinalReconhecido.Should().Be(idDoSinal);
        }

        private SinalEstatico DadoQueORepositorioRetorneUmSinalParaADescricao(string descricaoDoSinal)
        {
            var sinal = DadoUmSinalComDuasAmostras(descricaoDoSinal);

            repositorio
                .Setup(r => r.BuscarPorId(descricaoDoSinal))
                .Returns(sinal);

            return sinal;
        }

        private void DadoQueORepositorioRetorneNullParaADescricao(string descricaoDoSinal)
        {
            repositorio.Setup(r => r.BuscarPorId(descricaoDoSinal)).Returns((SinalEstatico)null);
        }

        private static SinalEstatico DadoUmSinalComDuasAmostras(string descricaoDoSinal)
        {
            var sinal = new SinalBuilder()
                            .ComDescricao(descricaoDoSinal)
                            .ComAmostra(new AmostraBuilder().Construir())
                            .ComAmostra(new AmostraBuilder().Construir())
                            .Construir();
            return sinal;
        }

        private void DeveTerAdicionadoOSinalNoRepositorioESalvoAsAlteracoes(SinalEstatico sinalEstatico)
        {
            repositorio.Verify(r => r.Adicionar(sinalEstatico));
            repositorio.Verify(r => r.SalvarAlteracoes());
        }

        private void DeveTerJuntadoAsAmostrasESalvoAsAlteracoes(SinalEstatico sinalAntigo, SinalEstatico novoSinal)
        {
            repositorio.Verify(r => r.Adicionar(It.IsAny<SinalEstatico>()), Times.Never);
            repositorio.Verify(r => r.SalvarAlteracoes());
            sinalAntigo.Amostras.Count.Should().Be(4);
            sinalAntigo.Amostras.Should().Contain(novoSinal.Amostras);
        }
    }
}
