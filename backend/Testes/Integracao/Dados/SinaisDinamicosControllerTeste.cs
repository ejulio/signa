using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;
using Dominio.Dados;
using Dominio.Dados.Repositorio;
using Dominio.Sinais;
using Dominio.Util;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Integracao.Dados
{
    [TestClass]
    public class SinaisDinamicosControllerTeste
    {
        private IRepositorio<Sinal> repositorio;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisDinamicos> algoritmoDeSinaisDinamicos;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos> algoritmoDeSinaisEstaticos;
        private SinaisDinamicosController sinaisDinamicosController;
        private GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicasComTipoFrame;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new RepositorioDeSinais(Caminhos.CaminhoDoArquivoDeAmostras);
            algoritmoDeSinaisEstaticos = new Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos>();
            algoritmoDeSinaisDinamicos = new Mock<IAlgoritmoDeReconhecimentoDeSinaisDinamicos>();

            var geradorDeCaracteristicas = new GeradorDeCaracteristicasDeSinalEstatico();
            geradorDeCaracteristicasComTipoFrame =
                new GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(geradorDeCaracteristicas);

            sinaisDinamicosController = new SinaisDinamicosController(repositorio, 
                geradorDeCaracteristicasComTipoFrame,
                algoritmoDeSinaisDinamicos.Object, 
                algoritmoDeSinaisEstaticos.Object);

            Directory.CreateDirectory(SinaisController.DiretorioDeExemplos);
        }

        [TestCleanup]
        public void DeletarArquivos()
        {
            if (Directory.Exists(SinaisController.DiretorioDeExemplos))
            {
                Directory.Delete(SinaisController.DiretorioDeExemplos, true);
            }
        }

        [TestMethod]
        public void adicionando_um_sinal_que_nao_existe()
        {
            var amostra = new ColecaoDeFramesBuilder().Construir();
            const string descricaoDoSinal = "Novo sinal";
            const string conteudoDoArquivo = "conteúdo do arquivo do novo sinal";

            sinaisDinamicosController.SalvarAmostraDoSinal(descricaoDoSinal, conteudoDoArquivo, amostra);

            var caminhoDoArquivoCriado = SinaisEstaticosController.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            DeveTerCriadoOArquivoComConteudo(caminhoDoArquivoCriado, descricaoDoSinal, conteudoDoArquivo);

            var sinalAdicionadoNoRepositorio = repositorio.BuscarPorDescricao(descricaoDoSinal);
            sinalAdicionadoNoRepositorio.Should().NotBeNull();
            sinalAdicionadoNoRepositorio.Amostras[0].Should().Contain(amostra);
        }

        [TestMethod]
        public void adicionando_um_sinal_que_existe()
        {
            const string descricaoDoSinal = "Novo sinal";
            const string conteudoAntigoDoArquivo = "conteúdo antigo do sinal";

            DadoQueExistaOSinalComConteudoNoArquivo(descricaoDoSinal, conteudoAntigoDoArquivo);
            var amostra = new ColecaoDeFramesBuilder().Construir();
            
            sinaisDinamicosController.SalvarAmostraDoSinal(descricaoDoSinal, "conteúdo novo do arquivo", amostra);

            NaoDeveTerAlteradoOConteudoDoArquivo(descricaoDoSinal, conteudoAntigoDoArquivo);
            var sinalAdicionadoNoRepositorio = repositorio.BuscarPorDescricao(descricaoDoSinal);
            sinalAdicionadoNoRepositorio.Should().NotBeNull();
            sinalAdicionadoNoRepositorio.Amostras[1].Should().Contain(amostra);
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int idDoSinal = 23;
            var amostra = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisDinamicos.Setup(a => a.Reconhecer(amostra)).Returns(idDoSinal);

            var resultado = sinaisDinamicosController.Reconhecer(idDoSinal, amostra);

            resultado.Should().BeTrue();
        }

        [TestMethod]
        public void reconhecendo_um_primeiro_frame()
        {
            const int idDoSinal = 23;
            var amostra = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisEstaticos.Setup(a => a.Reconhecer(amostra)).Returns(idDoSinal);

            var resultado = sinaisDinamicosController.ReconhecerPrimeiroFrame(idDoSinal, amostra);

            resultado.Should().BeTrue();
            geradorDeCaracteristicasComTipoFrame.TipoFrame.Should().Be(TipoFrame.Primeiro);
        }

        [TestMethod]
        public void reconhecendo_um_ultimo_frame()
        {
            const int idDoSinal = 23;
            var amostraPrimeiroFrame = new ColecaoDeFramesBuilder().Construir();
            var amostraUltimoFrame = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisEstaticos.Setup(a => a.Reconhecer(amostraUltimoFrame)).Returns(idDoSinal);

            var resultado = sinaisDinamicosController.ReconhecerUltimoFrame(idDoSinal, amostraPrimeiroFrame, amostraUltimoFrame);

            resultado.Should().BeTrue();
            geradorDeCaracteristicasComTipoFrame.TipoFrame.Should().Be(TipoFrame.Ultimo);
        }

        private static void DeveTerCriadoOArquivoComConteudo(string caminhoDoArquivoCriado, string descricaoDoSinal, string conteudoDoArquivo)
        {
            var caminhoDoArquivoCriadoEsperado = SinaisEstaticosController.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            caminhoDoArquivoCriado.Should().Be(caminhoDoArquivoCriadoEsperado);
            File.Exists(caminhoDoArquivoCriadoEsperado).Should().BeTrue();
            using (StreamReader reader = new StreamReader(caminhoDoArquivoCriadoEsperado))
            {
                reader.ReadToEnd().Should().Be(conteudoDoArquivo);
            }
        }

        private static void NaoDeveTerAlteradoOConteudoDoArquivo(string descricaoDoSinal, string conteudoDoArquivo)
        {
            var caminhoDoArquivoCriadoEsperado = SinaisEstaticosController.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            using (StreamReader reader = new StreamReader(caminhoDoArquivoCriadoEsperado))
            {
                reader.ReadToEnd().Should().Be(conteudoDoArquivo);
            }
        }

        private void DadoQueExistaOSinalComConteudoNoArquivo(string descricaoDoSinal, string conteudoDoArquivo)
        {
            var amostra = new ColecaoDeFramesBuilder().Construir();
            sinaisDinamicosController.SalvarAmostraDoSinal(descricaoDoSinal, conteudoDoArquivo, amostra);
        }
    }
}
