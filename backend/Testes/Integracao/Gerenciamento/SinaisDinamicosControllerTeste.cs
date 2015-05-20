using System.IO;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Estatico;
using Dominio.Gerenciamento;
using Dominio.Persistencia;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using Dominio.Util;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Integracao.Gerenciamento
{
    [TestClass]
    public class SinaisDinamicosControllerTeste
    {
        private IRepositorio<Sinal> repositorio;
        private Mock<IAlgoritmoClassificacaoSinaisDinamicos> algoritmoDeSinaisDinamicos;
        private Mock<IAlgoritmoClassificacaoSinaisEstaticos> algoritmoDeSinaisEstaticos;
        private GerenciadorSinaisDinamicos gerenciadorSinaisDinamicos;
        private CaracteristicasSinalEstaticoComTipoFrame caracteristicasComTipoFrame;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new RepositorioSinais(Caminhos.CaminhoDoArquivoDeAmostras);
            algoritmoDeSinaisEstaticos = new Mock<IAlgoritmoClassificacaoSinaisEstaticos>();
            algoritmoDeSinaisDinamicos = new Mock<IAlgoritmoClassificacaoSinaisDinamicos>();

            var geradorDeCaracteristicas = new CaracteristicasSinalEstatico();
            caracteristicasComTipoFrame =
                new CaracteristicasSinalEstaticoComTipoFrame(geradorDeCaracteristicas);

            gerenciadorSinaisDinamicos = new GerenciadorSinaisDinamicos(repositorio, 
                caracteristicasComTipoFrame,
                algoritmoDeSinaisDinamicos.Object, 
                algoritmoDeSinaisEstaticos.Object);

            Directory.CreateDirectory(GerenciadorSinais.DiretorioDeExemplos);
        }

        [TestCleanup]
        public void DeletarArquivos()
        {
            if (Directory.Exists(GerenciadorSinais.DiretorioDeExemplos))
            {
                Directory.Delete(GerenciadorSinais.DiretorioDeExemplos, true);
            }
        }

        [TestMethod]
        public void adicionando_um_sinal_que_nao_existe()
        {
            var amostra = new ColecaoDeFramesBuilder().Construir();
            const string descricaoDoSinal = "Novo sinal";
            const string conteudoDoArquivo = "conteúdo do arquivo do novo sinal";

            gerenciadorSinaisDinamicos.SalvarAmostraDoSinal(descricaoDoSinal, conteudoDoArquivo, amostra);

            var caminhoDoArquivoCriado = GerenciadorSinaisEstaticos.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
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
            
            gerenciadorSinaisDinamicos.SalvarAmostraDoSinal(descricaoDoSinal, "conteúdo novo do arquivo", amostra);

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
            algoritmoDeSinaisDinamicos.Setup(a => a.Classificar(amostra)).Returns(idDoSinal);

            var resultado = gerenciadorSinaisDinamicos.Reconhecer(idDoSinal, amostra);

            resultado.Should().BeTrue();
        }

        [TestMethod]
        public void reconhecendo_um_primeiro_frame()
        {
            const int idDoSinal = 23;
            var amostra = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisEstaticos.Setup(a => a.Classificar(amostra)).Returns(idDoSinal);

            var resultado = gerenciadorSinaisDinamicos.ReconhecerPrimeiroFrame(idDoSinal, amostra);

            resultado.Should().BeTrue();
            caracteristicasComTipoFrame.TipoFrame.Should().Be(TipoFrame.Primeiro);
        }

        [TestMethod]
        public void reconhecendo_um_ultimo_frame()
        {
            const int idDoSinal = 23;
            var amostraPrimeiroFrame = new ColecaoDeFramesBuilder().Construir();
            var amostraUltimoFrame = new ColecaoDeFramesBuilder().Construir();
            algoritmoDeSinaisEstaticos.Setup(a => a.Classificar(amostraUltimoFrame)).Returns(idDoSinal);

            var resultado = gerenciadorSinaisDinamicos.ReconhecerUltimoFrame(idDoSinal, amostraPrimeiroFrame, amostraUltimoFrame);

            resultado.Should().BeTrue();
            caracteristicasComTipoFrame.TipoFrame.Should().Be(TipoFrame.Ultimo);
        }

        private static void DeveTerCriadoOArquivoComConteudo(string caminhoDoArquivoCriado, string descricaoDoSinal, string conteudoDoArquivo)
        {
            var caminhoDoArquivoCriadoEsperado = GerenciadorSinaisEstaticos.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            caminhoDoArquivoCriado.Should().Be(caminhoDoArquivoCriadoEsperado);
            File.Exists(caminhoDoArquivoCriadoEsperado).Should().BeTrue();
            using (StreamReader reader = new StreamReader(caminhoDoArquivoCriadoEsperado))
            {
                reader.ReadToEnd().Should().Be(conteudoDoArquivo);
            }
        }

        private static void NaoDeveTerAlteradoOConteudoDoArquivo(string descricaoDoSinal, string conteudoDoArquivo)
        {
            var caminhoDoArquivoCriadoEsperado = GerenciadorSinaisEstaticos.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            using (StreamReader reader = new StreamReader(caminhoDoArquivoCriadoEsperado))
            {
                reader.ReadToEnd().Should().Be(conteudoDoArquivo);
            }
        }

        private void DadoQueExistaOSinalComConteudoNoArquivo(string descricaoDoSinal, string conteudoDoArquivo)
        {
            var amostra = new ColecaoDeFramesBuilder().Construir();
            gerenciadorSinaisDinamicos.SalvarAmostraDoSinal(descricaoDoSinal, conteudoDoArquivo, amostra);
        }
    }
}
