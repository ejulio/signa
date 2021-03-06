﻿using Dominio.Algoritmos.Treinamento;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Unidade.Algoritmos.Treinamento
{
    [TestClass]
    public class DadosFramesSinaisDinamicosTeste
    {
        [TestMethod]
        public void criando_dados_para_um_sinal_com_cinco_frames()
        {
            var sinais = DadaUmaColecaoComUmaAmostraDeCincoFrames();
            var sinal = sinais[0];

            var dados = new DadosFramesSinaisDinamicos(sinais);
            dados.Processar();
            var saidasEsperadas = DadasAsSaidasEsperadosParaAColecaoDeSinais(sinais);

            DeveTerExtraidoOsDadosDasAmostras(dados, 1, 1, saidasEsperadas, sinal.Amostras);
        }

        [TestMethod]
        public void criando_dados_para_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostras = 2;
            const int quantidadeDeSinais = 4;
            var colecaoDeSinais = DadaUmaColecaoDeSinaisComAmostras(quantidadeDeAmostras, quantidadeDeSinais);

            var dados = new DadosFramesSinaisDinamicos(colecaoDeSinais);
            dados.Processar();

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
                    var metadeDosFrames = amostra.Count / 2;
                    for (int i = 0; i < metadeDosFrames; i++)
                        saidasEsperadas.Add(sinal.IdNoAlgoritmo);

                    for (int i = metadeDosFrames; i < amostra.Count; i++)
                        saidasEsperadas.Add(colecaoDeSinais.Count + sinal.IdNoAlgoritmo);
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

        private void DeveTerExtraidoOsDadosDasAmostras(DadosFramesSinaisDinamicos dados, int quantidadeDeSinais, 
            int quantidadeDeAmostras, int[] saidasEsperadas, IList<IList<Frame>> amostrasEsperadas)
        {
            dados.CaracteristicasSinais.Should().HaveSameCount(saidasEsperadas);
            dados.IdentificadoresSinais.Should().HaveSameCount(saidasEsperadas);
            dados.IdentificadoresSinais.Should().ContainInOrder(saidasEsperadas);
            dados.QuantidadeClasses.Should().Be(quantidadeDeSinais * 2);
        }
    }
}