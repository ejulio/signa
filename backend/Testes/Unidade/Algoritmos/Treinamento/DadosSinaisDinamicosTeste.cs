using Dominio.Algoritmos.Treinamento;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;
using Testes.Comum.Util;

namespace Testes.Unidade.Algoritmos.Treinamento
{
    [TestClass]
    public class DadosSinaisDinamicosTeste
    {
        [TestMethod]
        public void criando_dados_para_um_sinal_com_cinco_frames()
        {
            var sinais = DadaUmaColecaoComUmSinalComCincoFrames();
            var sinal = sinais[0];

            var dados = new DadosSinaisDinamicos(sinais);
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

            var dados = new DadosSinaisDinamicos(colecaoDeSinais);
            
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
                    saidasEsperadas.Add(sinal.IdNoAlgoritmo);
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

        private void DeveTerExtraidoOsDadosDasAmostras(DadosSinaisDinamicos dados, int quantidadeDeSinais, 
            int quantidadeDeAmostras, int[] saidasEsperadas, IList<IList<Frame>> amostrasEsperadas)
        {
            dados.Entradas.Should().HaveCount(quantidadeDeSinais * quantidadeDeAmostras);
            dados.Saidas.Should().HaveSameCount(dados.Entradas);
            dados.Saidas.Should().ContainInOrder(saidasEsperadas);
            dados.QuantidadeDeClasses.Should().Be(quantidadeDeSinais);
            DeveTerAsEntradasDasAmostras(dados.Entradas, amostrasEsperadas);
        }

        private void DeveTerAsEntradasDasAmostras(double[][][] entradas, IList<IList<Frame>> amostras)
        {
            for (var i = 0; i < entradas.Length; i++)
            {
                for (var j = 0; j < entradas[i].Length; j++)
                {
                    entradas[i][j].Should().ContainInOrder(amostras[i][j].MontarArrayEsperadoParaSinaisEstaticos());
                }
            }
        }
    }
}