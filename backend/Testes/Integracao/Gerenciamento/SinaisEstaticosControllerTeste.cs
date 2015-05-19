using System.IO;
using Dominio.Gerenciamento;
using Dominio.Persistencia;
using Dominio.Sinais;
using Dominio.Util;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Integracao.Gerenciamento
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
        public void salvando_uma_amostra_de_um_sinal()
        {
            var amostra = new ColecaoDeFramesBuilder().Construir();
            const string descricaoDoSinal = "Novo sinal";
            const string conteudoDoArquivo = "conteúdo do arquivo do novo sinal";

            sinaisEstaticosController.SalvarAmostraDoSinal(descricaoDoSinal, conteudoDoArquivo, amostra);

            var caminhoDoArquivoCriado = SinaisController.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            DeveTerCriadoOArquivoComConteudo(caminhoDoArquivoCriado, descricaoDoSinal, conteudoDoArquivo);

            var sinalAdicionadoNoRepositorio = repositorio.BuscarPorDescricao(descricaoDoSinal);
            sinalAdicionadoNoRepositorio.Should().NotBeNull();
            sinalAdicionadoNoRepositorio.Amostras[0].Should().Contain(amostra);
        }

        private static void DeveTerCriadoOArquivoComConteudo(string caminhoDoArquivoCriado, string descricaoDoSinal, string conteudoDoArquivo)
        {
            var caminhoDoArquivoCriadoEsperado = SinaisController.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            caminhoDoArquivoCriado.Should().Be(caminhoDoArquivoCriadoEsperado);
            File.Exists(caminhoDoArquivoCriadoEsperado).Should().BeTrue();
            using (StreamReader reader = new StreamReader(caminhoDoArquivoCriadoEsperado))
            {
                reader.ReadToEnd().Should().Be(conteudoDoArquivo);
            }
        }

        private static void NaoDeveTerTrocadoOConteudoDoArquivo(string descricaoDoSinal, string conteudoAntigoDoArquivo)
        {
            var file = SinaisController.DiretorioDeExemplos + descricaoDoSinal.Underscore() + ".json";
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadToEnd().Should().Be(conteudoAntigoDoArquivo);
            }
        }
    }
}
