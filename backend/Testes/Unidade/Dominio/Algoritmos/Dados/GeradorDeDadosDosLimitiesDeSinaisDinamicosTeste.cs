﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dados;
using Signa.Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;
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
            var saidasEsperadas = new [] { 0, 0 };

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
            var saidasEsperadas = new[] { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 };

            DeveTerExtraidoOsDadosDasAmostras(dados, quantidadeDeSinais, quantidadeDeAmostras, saidasEsperadas, amostrasEsperadas);
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

            var sinais = new[] { new SinalBuilder().ComAmostra(frames).Construir() };
            return sinais;
        }

        private static ICollection<Sinal> DadaUmaColecaoDeSinaisComAmostras(int quantidadeDeAmostras, int quantidadeDeSinais)
        {
            var colecaoDeSinais = new ColecaoDeSinaisBuilder()
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
            geradorDeDados.QuantidadeDeClasses.Should().Be(quantidadeDeSinais * 2);
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