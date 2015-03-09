using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Estatico
{
    [TestClass]
    public class DadosParaAlgoritmoDeReconhecimentoDeSinalTeste
    {
        private readonly Amostra amostraPadrao;

        public DadosParaAlgoritmoDeReconhecimentoDeSinalTeste()
        {
            var frames = new[] {new FrameBuilder().Construir()};
            amostraPadrao = new AmostraBuilder().ComFrames(frames).Construir();
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_um_sinal()
        {
            var sinais = DadaUmaColecaoDeUmSinal();

            var dadosDoAlgoritmo = new DadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos(sinais);

            DeveTerUmDadoDeTreinamento(dadosDoAlgoritmo);
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostrasPorSinal = 5;
            const int quantidadeDeSinais = 6;

            var sinais = DadaUmaColecaoDeSinais(quantidadeDeAmostrasPorSinal, quantidadeDeSinais);

            var dadosDoAlgoritmo = new DadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos(sinais);

            DeveTerOsDadosDaColecaoDeSinais(dadosDoAlgoritmo, sinais, quantidadeDeAmostrasPorSinal);
        }

        private ICollection<Sinal> DadaUmaColecaoDeUmSinal()
        {
            Func<int, Amostra> geradorDeAmostras = index => amostraPadrao;

            var sinais = new ColecaoDeSinaisBuilder()
                        .ComQuantidadeDeAmostrasPorSinal(1)
                        .ComGeradorDeAmostras(geradorDeAmostras)
                        .ComQuantidadeDeSinais(1)
                        .Construir();

            return sinais;
        }

        private static ICollection<Sinal> DadaUmaColecaoDeSinais(int quantidadeDeAmostrasPorSinal, int quantidadeDeSinais)
        {
            var sinais = new ColecaoDeSinaisBuilder()
                        .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostrasPorSinal)
                        .ComQuantidadeDeSinais(quantidadeDeSinais)
                        .ComGeradorDeAmostrasEstaticas()
                        .Construir();

            return sinais;
        }

        private void DeveTerOsDadosDaColecaoDeSinais(DadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dadosDoAlgoritmo, ICollection<Sinal> sinais, int quantidadeDeAmostrasPorSinal)
        {
            dadosDoAlgoritmo.QuantidadeDeClasses.Should().Be(sinais.Count);
            dadosDoAlgoritmo.Entradas.Should().HaveCount(quantidadeDeAmostrasPorSinal * sinais.Count);
            dadosDoAlgoritmo.Entradas.Should().HaveSameCount(dadosDoAlgoritmo.Saidas);
            dadosDoAlgoritmo.Saidas.Should().ContainInOrder(SaidasEsperadas(sinais.Count, quantidadeDeAmostrasPorSinal));

            int indiceDaEntrada = 0;
            var entradasEsperadas = EntradasEsperadasParaOsSinais(sinais);
            foreach (var input in dadosDoAlgoritmo.Entradas)
            {
                input.Should().ContainInOrder(entradasEsperadas[indiceDaEntrada]);
                indiceDaEntrada++;
            }
        }

        private void DeveTerUmDadoDeTreinamento(DadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dadosDoAlgoritmo)
        {
            var arrayDaAmostra = (amostraPadrao as IAmostraDeSinalEstatico).ParaArray();
            dadosDoAlgoritmo.QuantidadeDeClasses.Should().Be(1);
            dadosDoAlgoritmo.Entradas.Should().HaveCount(1);
            dadosDoAlgoritmo.Entradas[0].Should().ContainInOrder(arrayDaAmostra);
            dadosDoAlgoritmo.Saidas.Should().HaveCount(1);
            dadosDoAlgoritmo.Saidas[0].Should().Be(0);
        }

        private double[][] EntradasEsperadasParaOsSinais(ICollection<Sinal> sinais)
        {
            var entradasEsperadas = new LinkedList<double[]>();

            foreach (var sinal in sinais)
            {
                foreach (IAmostraDeSinalEstatico amostra in sinal.Amostras)
                {
                    entradasEsperadas.AddFirst(amostra.ParaArray());
                }
            }

            return entradasEsperadas.ToArray();
        }

        private IEnumerable SaidasEsperadas(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            int[] saidas = new int[quantidadeDeSinais * quantidadeDeAmostrasPorSinal];
            int indiceDoSinal = quantidadeDeSinais - 1;

            int i = 0;
            while (i < saidas.Length)
            {
                saidas[i] = indiceDoSinal;

                i++;

                if (i % quantidadeDeAmostrasPorSinal == 0)
                {
                    indiceDoSinal--;
                }
            }

            return saidas;
        }
    }
}
