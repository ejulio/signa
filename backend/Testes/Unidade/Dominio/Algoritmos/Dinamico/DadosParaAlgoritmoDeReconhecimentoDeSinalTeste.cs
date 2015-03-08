using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Dinamico
{
    [TestClass]
    public class DadosParaAlgoritmoDeReconhecimentoDeSinalTeste
    {
        [TestMethod]
        public void criando_dados_para_um_sinal_com_cinco_frames()
        {
            var sinais = DadaUmaColecaoComUmSinalComCincoFrames();
            var sinal = sinais[0];

            var dados = new DadosParaAlgoritmoDeReconhecimentoDeSinal(sinais);
            var saidasEsperadas = new [] { 0 };

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

            var amostra = new AmostraBuilder().ComFrames(frames).Construir();
            var sinais = new[] {new SinalBuilder().ComAmostra(amostra).Construir()};
            return sinais;
        }

        [TestMethod]
        public void criando_dados_para_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostras = 2;
            const int quantidadeDeSinais = 4;
            var colecaoDeSinais = DadaUmaColecaoDeSinaisComAmostras(quantidadeDeAmostras, quantidadeDeSinais);

            var dados = new DadosParaAlgoritmoDeReconhecimentoDeSinal(colecaoDeSinais);
            
            var amostrasEsperadas = ConcatenarAmostrasDosSinais(colecaoDeSinais);
            var saidasEsperadas = new[] { 0, 0, 1, 1, 2, 2, 3, 3 };

            DeveTerExtraidoOsDadosDasAmostras(dados, quantidadeDeSinais, quantidadeDeAmostras, saidasEsperadas, amostrasEsperadas);
        }

        private static ICollection<Sinal> DadaUmaColecaoDeSinaisComAmostras(int quantidadeDeAmostras, int quantidadeDeSinais)
        {
            var colecaoDeSinais = new ColecaoDeSinaisDinamicosBuilder()
                .ComTemplateDeDescricao("Sinal dinâmico {0}")
                .ComTemplateDeCaminho("sinal-dinamico-{0}.json")
                .ComQuantidadeDeAmostras(quantidadeDeAmostras)
                .ComTamanho(quantidadeDeSinais)
                .Construir();

            return colecaoDeSinais;
        }

        private IList<Amostra> ConcatenarAmostrasDosSinais(ICollection<Sinal> colecaoDeSinais)
        {
            IEnumerable<Amostra> amostrasConcatenadas = new Amostra[0];

            foreach (var sinal in colecaoDeSinais)
            {
                amostrasConcatenadas = amostrasConcatenadas.Concat(sinal.Amostras);
            }

            return amostrasConcatenadas.ToArray();
        }

        private void DeveTerExtraidoOsDadosDasAmostras(DadosParaAlgoritmoDeReconhecimentoDeSinal dados, int quantidadeDeSinais, 
            int quantidadeDeAmostras, int[] saidasEsperadas, IList<Amostra> amostrasEsperadas)
        {
            dados.Entradas.Should().HaveCount(quantidadeDeSinais * quantidadeDeAmostras);
            dados.Saidas.Should().HaveSameCount(dados.Entradas);
            dados.Saidas.Should().ContainInOrder(saidasEsperadas);
            dados.QuantidadeDeClasses.Should().Be(quantidadeDeSinais);
            DeveTerAsEntradasDasAmostras(dados.Entradas, amostrasEsperadas);
        }

        private void DeveTerAsEntradasDasAmostras(double[][][] entradas, IList<Amostra> amostras)
        {
            for (var i = 0; i < entradas.Length; i++)
            {
                for (var j = 0; j < entradas[i].Length; j++)
                {
                    entradas[i][j].Should().ContainInOrder(amostras[i].Frames[j].ToArray());
                }
            }
        }
    }
}