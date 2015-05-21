using Dominio.Algoritmos.Dados;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais.Frames;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;
using Testes.Comum.Util;

namespace Testes.Unidade.Algoritmos.Dados
{
    [TestClass]
    public class GeradorDeDadosDeSinaisEstaticosTeste
    {
        private readonly IList<Frame> amostraPadrao;

        public GeradorDeDadosDeSinaisEstaticosTeste()
        {
            amostraPadrao = new[] {new FrameBuilder().Construir()};
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_um_sinal()
        {
            var sinais = DadaUmaColecaoDeUmSinal();

            var dadosDoAlgoritmo = new DadosSinaisEstaticos(sinais);

            DeveTerUmDadoDeTreinamento(sinais.First(), dadosDoAlgoritmo);
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostrasPorSinal = 5;
            const int quantidadeDeSinais = 6;

            var sinais = DadaUmaColecaoDeSinais(quantidadeDeAmostrasPorSinal, quantidadeDeSinais);

            var dadosDoAlgoritmo = new DadosSinaisEstaticos(sinais);

            DeveTerOsDadosDaColecaoDeSinais(dadosDoAlgoritmo, sinais, quantidadeDeAmostrasPorSinal);
        }

        private ICollection<Sinal> DadaUmaColecaoDeUmSinal()
        {
            Func<int, IList<Frame>> geradorDeAmostras = index => amostraPadrao;

            var sinais = new ColecaoDeSinaisBuilder()
                        .ComGeradorDeId(i => 1)
                        .ComQuantidadeDeAmostrasPorSinal(1)
                        .ComGeradorDeAmostras(geradorDeAmostras)
                        .ComQuantidadeDeSinais(1)
                        .Construir();

            return sinais;
        }

        private static ICollection<Sinal> DadaUmaColecaoDeSinais(int quantidadeDeAmostrasPorSinal, int quantidadeDeSinais)
        {
            var sinais = new ColecaoDeSinaisBuilder()
                        .ComGeradorDeId(i => i + 7)
                        .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostrasPorSinal)
                        .ComQuantidadeDeSinais(quantidadeDeSinais)
                        .ComGeradorDeAmostrasEstaticas()
                        .Construir();

            return sinais;
        }

        private void DeveTerOsDadosDaColecaoDeSinais(DadosSinaisEstaticos dadosDoAlgoritmo, ICollection<Sinal> sinais, int quantidadeDeAmostrasPorSinal)
        {
            dadosDoAlgoritmo.QuantidadeDeClasses.Should().Be(sinais.Count);
            dadosDoAlgoritmo.Entradas.Should().HaveCount(quantidadeDeAmostrasPorSinal * sinais.Count);
            dadosDoAlgoritmo.Entradas.Should().HaveSameCount(dadosDoAlgoritmo.Saidas);
            dadosDoAlgoritmo.Saidas.Should().ContainInOrder(SaidasEsperadas(sinais));

            int indiceDaEntrada = 0;
            var entradasEsperadas = EntradasEsperadasParaOsSinais(sinais);
            foreach (var input in dadosDoAlgoritmo.Entradas)
            {
                input.Should().ContainInOrder(entradasEsperadas[indiceDaEntrada]);
                indiceDaEntrada++;
            }
        }

        private void DeveTerUmDadoDeTreinamento(Sinal sinal, DadosSinaisEstaticos dadosDoAlgoritmo)
        {
            var arrayDaAmostra = amostraPadrao[0].MontarArrayEsperadoParaSinaisEstaticos();
            dadosDoAlgoritmo.QuantidadeDeClasses.Should().Be(1);
            dadosDoAlgoritmo.Entradas.Should().HaveCount(1);
            dadosDoAlgoritmo.Entradas[0].Should().ContainInOrder(arrayDaAmostra);
            dadosDoAlgoritmo.Saidas.Should().HaveCount(1);
            dadosDoAlgoritmo.Saidas[0].Should().Be(sinal.IdNoAlgoritmo);
        }

        private double[][] EntradasEsperadasParaOsSinais(ICollection<Sinal> sinais)
        {
            var entradasEsperadas = new LinkedList<double[]>();

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    entradasEsperadas.AddLast(amostra[0].MontarArrayEsperadoParaSinaisEstaticos());
                }
            }

            return entradasEsperadas.ToArray();
        }

        private int[] SaidasEsperadas(ICollection<Sinal> sinais)
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
    }
}
