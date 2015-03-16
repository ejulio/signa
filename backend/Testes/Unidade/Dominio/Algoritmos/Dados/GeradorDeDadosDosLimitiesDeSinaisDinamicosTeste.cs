using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Dominio.Algoritmos.Dados;
using Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Util;

namespace Testes.Unidade.Dominio.Algoritmos.Dados
{
    [TestClass]
    public class GeradorDeDadosDosLimitiesDeSinaisDinamicosTeste
    {
        [TestMethod]
        public void criando_dados_para_um_sinal_com_cinco_frames()
        {
            var sinais = DadaUmaColecaoComUmaAmostraDeCincoFrames();
            var sinal = sinais[0];

            var dados = new GeradorDeDadosDosLimitesDeSinaisDinamicos(sinais);
            var saidasEsperadas = DadasAsSaidasEsperadosParaAColecaoDeSinais(sinais);

            DeveTerExtraidoOsDadosDasAmostras(dados, 1, 1, saidasEsperadas, sinal.Amostras);
        }

        [TestMethod]
        public void criando_dados_para_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostras = 2;
            const int quantidadeDeSinais = 4;
            var colecaoDeSinais = DadaUmaColecaoDeSinaisComAmostras(quantidadeDeAmostras, quantidadeDeSinais);

            var dados = new GeradorDeDadosDosLimitesDeSinaisDinamicos(colecaoDeSinais);
            
            var amostrasEsperadas = ConcatenarAmostrasDosSinais(colecaoDeSinais);
            var saidasEsperadas = DadasAsSaidasEsperadosParaAColecaoDeSinais(colecaoDeSinais);

            DeveTerExtraidoOsDadosDasAmostras(dados, quantidadeDeSinais, quantidadeDeAmostras, saidasEsperadas, amostrasEsperadas);
        }

        private int[] DadasAsSaidasEsperadosParaAColecaoDeSinais(ICollection<Sinal> colecaoDeSinais)
        {
            var saidasEsperadas = new List<int>();
            foreach (var sinal in colecaoDeSinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    saidasEsperadas.Add(sinal.Id);
                    saidasEsperadas.Add(sinal.Id);
                }
            }

            return saidasEsperadas.ToArray();
        }

        private static Sinal[] DadaUmaColecaoComUmaAmostraDeCincoFrames()
        {
            var frames = new[]
            {
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir(),
                new FrameBuilder().ComMaosEsquerdaEDireitaPadroes().Construir()
            };

            var sinais = new[] { new SinalBuilder().ComId(1).ComAmostra(frames).Construir() };
            return sinais;
        }

        private static ICollection<Sinal> DadaUmaColecaoDeSinaisComAmostras(int quantidadeDeAmostras, int quantidadeDeSinais)
        {
            var colecaoDeSinais = new ColecaoDeSinaisBuilder()
                .ComGeradorDeId(i => i + 2)
                .ComTemplateDeDescricao("Sinal dinâmico {0}")
                .ComTemplateDoCaminhoDoArquivoDeExemplo("sinal-dinamico-{0}.json")
                .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostras)
                .ComQuantidadeDeSinais(quantidadeDeSinais)
                .Construir();

            return colecaoDeSinais;
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

        private void DeveTerExtraidoOsDadosDasAmostras(GeradorDeDadosDosLimitesDeSinaisDinamicos geradorDeDados, int quantidadeDeSinais, 
            int quantidadeDeAmostras, int[] saidasEsperadas, IList<IList<Frame>> amostrasEsperadas)
        {
            geradorDeDados.Entradas.Should().HaveCount(quantidadeDeSinais * quantidadeDeAmostras * 2);
            geradorDeDados.Saidas.Should().HaveSameCount(geradorDeDados.Entradas);
            geradorDeDados.Saidas.Should().ContainInOrder(saidasEsperadas);
            geradorDeDados.QuantidadeDeClasses.Should().Be(quantidadeDeSinais);
            DeveTerAsEntradasDasAmostras(geradorDeDados.Entradas, amostrasEsperadas);
        }

        private void DeveTerAsEntradasDasAmostras(double[][] entradas, IList<IList<Frame>> amostras)
        {
            int j = 0;
            for (var i = 0; i < entradas.Length; i += 2)
            {
                entradas[i].Should().ContainInOrder(amostras[j].First().MontarArrayEsperado(TipoFrame.Primeiro));
                entradas[i + 1].Should().ContainInOrder(amostras[j].Last().MontarArrayEsperado(TipoFrame.Ultimo));
                j++;
            }
        }
    }
}