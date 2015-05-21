using Dominio.Algoritmos.Treinamento;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;
using Testes.Comum.Util;

namespace Testes.Unidade.Algoritmos.Treinamento
{
    [TestClass]
    public class DadosSinaisEstaticosTeste
    {
        private readonly IList<Frame> amostraPadrao;

        public DadosSinaisEstaticosTeste()
        {
            amostraPadrao = new[] {new FrameBuilder().Construir()};
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_um_sinal()
        {
            var sinais = DadaUmaColecaoDeUmSinal();

            var dadosDoAlgoritmo = new DadosSinaisEstaticos(sinais);
            dadosDoAlgoritmo.Processar();

            DeveTerUmDadoDeTreinamento(sinais.First(), dadosDoAlgoritmo);
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostrasPorSinal = 5;
            const int quantidadeDeSinais = 6;

            var sinais = DadaUmaColecaoDeSinais(quantidadeDeAmostrasPorSinal, quantidadeDeSinais);

            var dadosDoAlgoritmo = new DadosSinaisEstaticos(sinais);
            dadosDoAlgoritmo.Processar();
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
            dadosDoAlgoritmo.QuantidadeClasses.Should().Be(sinais.Count);
            dadosDoAlgoritmo.CaracteristicasSinais.Should().HaveCount(quantidadeDeAmostrasPorSinal * sinais.Count);
            dadosDoAlgoritmo.CaracteristicasSinais.Should().HaveSameCount(dadosDoAlgoritmo.IdentificadoresSinais);
            dadosDoAlgoritmo.IdentificadoresSinais.Should().ContainInOrder(SaidasEsperadas(sinais));

            int indiceDaEntrada = 0;
            var entradasEsperadas = EntradasEsperadasParaOsSinais(sinais);
            foreach (var input in dadosDoAlgoritmo.CaracteristicasSinais)
            {
                input.Should().ContainInOrder(entradasEsperadas[indiceDaEntrada]);
                indiceDaEntrada++;
            }
        }

        private void DeveTerUmDadoDeTreinamento(Sinal sinal, DadosSinaisEstaticos dadosDoAlgoritmo)
        {
            var arrayDaAmostra = amostraPadrao[0].MontarArrayEsperadoParaSinaisEstaticos();
            dadosDoAlgoritmo.QuantidadeClasses.Should().Be(1);
            dadosDoAlgoritmo.CaracteristicasSinais.Should().HaveCount(1);
            dadosDoAlgoritmo.CaracteristicasSinais[0].Should().ContainInOrder(arrayDaAmostra);
            dadosDoAlgoritmo.IdentificadoresSinais.Should().HaveCount(1);
            dadosDoAlgoritmo.IdentificadoresSinais[0].Should().Be(sinal.IdNoAlgoritmo);
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
