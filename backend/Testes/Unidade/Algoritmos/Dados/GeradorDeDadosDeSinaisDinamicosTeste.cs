using Dominio.Algoritmos.Dados;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Util;

namespace Testes.Unidade.Algoritmos.Dados
{
    [TestClass]
    public class GeradorDeDadosDeSinaisDinamicosTeste
    {
        [TestMethod]
        public void criando_dados_para_um_sinal_com_cinco_frames()
        {
            var sinais = DadaUmaColecaoComUmSinalComCincoFrames();
            var sinal = sinais[0];

            var dados = new GeradorDeDadosDeSinaisDinamicos(sinais);
            var saidasEsperadas = DadasAsSaidasEsperadasParaAColecaoDeSinais(sinais);

            DeveTerExtraidoOsDadosDasAmostras(dados, 1, 1, saidasEsperadas, sinal.Amostras);
        }

        private static Sinal[] DadaUmaColecaoComUmSinalComCincoFrames()
        {
            var frames = new[]
            {
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir()
            };

            var sinais = new[] { new SinalBuilder().ComId(2).ComAmostra(frames).Construir() };
            return sinais;
        }

        [TestMethod]
        public void criando_dados_para_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostras = 2;
            const int quantidadeDeSinais = 4;
            var colecaoDeSinais = DadaUmaColecaoDeSinaisComAmostras(quantidadeDeAmostras, quantidadeDeSinais);

            var dados = new GeradorDeDadosDeSinaisDinamicos(colecaoDeSinais);
            
            var amostrasEsperadas = ConcatenarAmostrasDosSinais(colecaoDeSinais);
            var saidasEsperadas = DadasAsSaidasEsperadasParaAColecaoDeSinais(colecaoDeSinais);

            DeveTerExtraidoOsDadosDasAmostras(dados, quantidadeDeSinais, quantidadeDeAmostras, saidasEsperadas, amostrasEsperadas);
        }

        private static ICollection<Sinal> DadaUmaColecaoDeSinaisComAmostras(int quantidadeDeAmostras, int quantidadeDeSinais)
        {
            var colecaoDeSinais = new ColecaoDeSinaisBuilder()
                .ComGeradorDeId(i => i * 2)
                .ComTemplateDeDescricao("Sinal dinâmico {0}")
                .ComTemplateDoCaminhoDoArquivoDeExemplo("sinal-dinamico-{0}.json")
                .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostras)
                .ComQuantidadeDeSinais(quantidadeDeSinais)
                .Construir();

            return colecaoDeSinais;
        }

        private int[] DadasAsSaidasEsperadasParaAColecaoDeSinais(ICollection<Sinal> sinais)
        {
            var saidasEsperadas = new List<int>();

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    saidasEsperadas.Add(sinal.Id);
                }
            }

            return saidasEsperadas.ToArray();
        }

        private IList<IList<Frame>> ConcatenarAmostrasDosSinais(ICollection<Sinal> colecaoDeSinais)
        {
            IEnumerable<IList<Frame>> amostrasConcatenadas = new Frame[0][];

            foreach (var sinal in colecaoDeSinais)
            {
                amostrasConcatenadas = amostrasConcatenadas.Concat(sinal.Amostras);
            }

            return amostrasConcatenadas.ToArray();
        }

        private void DeveTerExtraidoOsDadosDasAmostras(GeradorDeDadosDeSinaisDinamicos geradorDeDados, int quantidadeDeSinais, 
            int quantidadeDeAmostras, int[] saidasEsperadas, IList<IList<Frame>> amostrasEsperadas)
        {
            geradorDeDados.Entradas.Should().HaveCount(quantidadeDeSinais * quantidadeDeAmostras);
            geradorDeDados.Saidas.Should().HaveSameCount(geradorDeDados.Entradas);
            geradorDeDados.Saidas.Should().ContainInOrder(saidasEsperadas);
            geradorDeDados.QuantidadeDeClasses.Should().Be(quantidadeDeSinais);
            DeveTerAsEntradasDasAmostras(geradorDeDados.Entradas, amostrasEsperadas);
        }

        private void DeveTerAsEntradasDasAmostras(double[][][] entradas, IList<IList<Frame>> amostras)
        {
            for (var i = 0; i < entradas.Length; i++)
            {
                for (var j = 0; j < entradas[i].Length; j++)
                {
                    entradas[i][j].Should().ContainInOrder(amostras[i][j].MontarArrayEsperado());
                }
            }
        }
    }
}