using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Dominio.Sinais;
using Signa.Util;
using System.IO;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Integracao.Dados
{
    [TestClass]
    public class SinaisEstaticosControllerTeste
    {
        private const string CaminhoParaOArquivoDeAmostras = Caminhos.CaminhoDoArquivoDeAmostras;
        private IRepositorio<Sinal> repositorio;
        private SinaisEstaticosController sinaisEstaticosController;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new RepositorioDeSinaisEstaticos(new RepositorioDeSinais(CaminhoParaOArquivoDeAmostras));
            sinaisEstaticosController = new SinaisEstaticosController(repositorio, null);

            Directory.CreateDirectory(SinaisEstaticosController.DiretorioDeAmostras);
        }

        [TestCleanup]
        public void DeletarArquivos()
        {
            if (Directory.Exists(SinaisEstaticosController.DiretorioDeAmostras))
            {
                Directory.Delete(SinaisEstaticosController.DiretorioDeAmostras, true);
            }
        }

        [TestMethod]
        public void criando_arquivo_de_exemplo_quando_nao_existe()
        {
            const string descricaoDoSinal = "new sign";
            const string dadosDoArquivo = "file data";

            var caminhoDoArquivoCriado = sinaisEstaticosController.CriarArquivoDeExemploSeNaoExistir(descricaoDoSinal, dadosDoArquivo);

            DeveTerCriadoOArquivoComConteudo(caminhoDoArquivoCriado, descricaoDoSinal, dadosDoArquivo);
        }

        [TestMethod]
        public void criando_um_arquivo_de_exemplo_quando_ja_existe()
        {
            const string descricaoDoSinal = "old sign";
            const string conteudoAntigoDoArquivo = "Old file data";
            const string conteudoNovoDoArquivo = "New file data";

            DadoQueExistaUmArquivoDeExemplo(descricaoDoSinal, conteudoAntigoDoArquivo);

            sinaisEstaticosController.CriarArquivoDeExemploSeNaoExistir(descricaoDoSinal, conteudoNovoDoArquivo);

            NaoDeveTerTrocadoOConteudoDoArquivo(descricaoDoSinal, conteudoAntigoDoArquivo);
        }

        [TestMethod]
        public void salvando_uma_amostra_de_um_sinal()
        {
            var amostra = new FrameBuilder().Construir();
            const string descricaoDoSinal = "Novo sinal";
            const string conteudoDoArquivo = "conteúdo do arquivo do novo sinal";

            sinaisEstaticosController.SalvarAmostraDoSinal(descricaoDoSinal, conteudoDoArquivo, amostra);

            var caminhoDoArquivoCriado = SinaisEstaticosController.DiretorioDeAmostras + descricaoDoSinal.Underscore() + ".json";
            DeveTerCriadoOArquivoComConteudo(caminhoDoArquivoCriado, descricaoDoSinal, conteudoDoArquivo);

            var sinalAdicionadoNoRepositorio = repositorio.BuscarPorDescricao(descricaoDoSinal);
            sinalAdicionadoNoRepositorio.Should().NotBeNull();
            sinalAdicionadoNoRepositorio.Amostras[0].Should().Contain(amostra);
        }

        private void DadoQueExistaUmArquivoDeExemplo(string descricaoDoSinal, string conteudoDoArquivoDoSinal)
        {
            sinaisEstaticosController.CriarArquivoDeExemploSeNaoExistir(descricaoDoSinal, conteudoDoArquivoDoSinal);
        }

        private static void DeveTerCriadoOArquivoComConteudo(string caminhoDoArquivoCriado, string descricaoDoSinal, string conteudoDoArquivo)
        {
            var caminhoDoArquivoCriadoEsperado = SinaisEstaticosController.DiretorioDeAmostras + descricaoDoSinal.Underscore() + ".json";
            caminhoDoArquivoCriado.Should().Be(caminhoDoArquivoCriadoEsperado);
            File.Exists(caminhoDoArquivoCriadoEsperado).Should().BeTrue();
            using (StreamReader reader = new StreamReader(caminhoDoArquivoCriadoEsperado))
            {
                reader.ReadToEnd().Should().Be(conteudoDoArquivo);
            }
        }

        private static void NaoDeveTerTrocadoOConteudoDoArquivo(string descricaoDoSinal, string conteudoAntigoDoArquivo)
        {
            var file = SinaisEstaticosController.DiretorioDeAmostras + descricaoDoSinal.Underscore() + ".json";
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadToEnd().Should().Be(conteudoAntigoDoArquivo);
            }
        }
    }
}
