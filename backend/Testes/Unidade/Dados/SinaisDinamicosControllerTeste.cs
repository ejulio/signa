using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dados
{
    [TestClass]
    public class SinaisDinamicosControllerTeste
    {
        private Mock<IRepositorio<Sinal>> repositorio;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisDinamicos> algoritmoDeSinaisDinamicos;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos> algoritmoDeSinaisEstaticos;
        private SinaisDinamicosController sinaisDinamicosController;
        private GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicasComTipoFrame;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new Mock<IRepositorio<Sinal>>();
            algoritmoDeSinaisEstaticos = new Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos>();
            algoritmoDeSinaisDinamicos = new Mock<IAlgoritmoDeReconhecimentoDeSinaisDinamicos>();

            var geradorDeCaracteristicas = new GeradorDeCaracteristicasDeSinalEstatico();
            geradorDeCaracteristicasComTipoFrame =
                new GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(geradorDeCaracteristicas);

            sinaisDinamicosController = new SinaisDinamicosController(repositorio.Object, 
                geradorDeCaracteristicasComTipoFrame,
                algoritmoDeSinaisDinamicos.Object, 
                algoritmoDeSinaisEstaticos.Object);
        }

        [TestMethod]
        public void adicionando_um_sinal_que_nao_existe()
        {
            //const string descricaoDoSinal = "new sign";
            //DadoQueORepositorioRetorneNullParaADescricao(descricaoDoSinal);

            //var sinal = DadoUmSinalComDuasAmostras(descricaoDoSinal);

            //sinaisDinamicosController.Adicionar(sinal);

            //DeveTerAdicionadoOSinalNoRepositorioESalvoAsAlteracoes(sinal);
        }

        [TestMethod]
        public void adicionando_um_sinal_que_ja_existe()
        {
            //const string descricaoDoSinal = "old sign";
            //var sinalAntigo = DadoQueORepositorioRetorneUmSinalParaADescricao(descricaoDoSinal);

            //var novoSinal = DadoUmSinalComDuasAmostras(descricaoDoSinal);

            //sinaisEstaticosController.Adicionar(novoSinal);

            //DeveTerJuntadoAsAmostrasESalvoAsAlteracoes(sinalAntigo, novoSinal);
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int idDoSinal = 23;
            var amostra = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisDinamicos.Setup(a => a.Reconhecer(amostra)).Returns(idDoSinal);

            var sinalReconhecido = sinaisDinamicosController.Reconhecer(amostra);

            sinalReconhecido.Should().Be(idDoSinal);
        }

        [TestMethod]
        public void reconhecendo_um_primeiro_frame()
        {
            const int idDoSinal = 23;
            var amostra = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisEstaticos.Setup(a => a.Reconhecer(amostra)).Returns(idDoSinal);

            var sinalReconhecido = sinaisDinamicosController.ReconhecerPrimeiroFrame(amostra);

            sinalReconhecido.Should().Be(idDoSinal);
            geradorDeCaracteristicasComTipoFrame.TipoFrame.Should().Be(TipoFrame.Primeiro);
        }

        [TestMethod]
        public void reconhecendo_um_ultimo_frame()
        {
            const int idDoSinal = 23;
            var amostra = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisEstaticos.Setup(a => a.Reconhecer(amostra)).Returns(idDoSinal);

            var sinalReconhecido = sinaisDinamicosController.ReconhecerUltimoFrame(amostra);

            sinalReconhecido.Should().Be(idDoSinal);
            geradorDeCaracteristicasComTipoFrame.TipoFrame.Should().Be(TipoFrame.Ultimo);
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
                            .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                            .ComAmostra(new ColecaoDeFramesBuilder().Construir())
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
