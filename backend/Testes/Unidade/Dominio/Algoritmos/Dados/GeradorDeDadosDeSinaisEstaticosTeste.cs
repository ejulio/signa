﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dados;
using Signa.Dominio.Sinais;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Util;

namespace Testes.Unidade.Dominio.Algoritmos.Dados
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

            var dadosDoAlgoritmo = new GeradorDeDadosDeSinaisEstaticos(sinais);

            DeveTerUmDadoDeTreinamento(dadosDoAlgoritmo);
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostrasPorSinal = 5;
            const int quantidadeDeSinais = 6;

            var sinais = DadaUmaColecaoDeSinais(quantidadeDeAmostrasPorSinal, quantidadeDeSinais);

            var dadosDoAlgoritmo = new GeradorDeDadosDeSinaisEstaticos(sinais);

            DeveTerOsDadosDaColecaoDeSinais(dadosDoAlgoritmo, sinais, quantidadeDeAmostrasPorSinal);
        }

        private ICollection<Sinal> DadaUmaColecaoDeUmSinal()
        {
            Func<int, IList<Frame>> geradorDeAmostras = index => amostraPadrao;

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

        private void DeveTerOsDadosDaColecaoDeSinais(GeradorDeDadosDeSinaisEstaticos geradorDeDadosDoAlgoritmo, ICollection<Sinal> sinais, int quantidadeDeAmostrasPorSinal)
        {
            geradorDeDadosDoAlgoritmo.QuantidadeDeClasses.Should().Be(sinais.Count);
            geradorDeDadosDoAlgoritmo.Entradas.Should().HaveCount(quantidadeDeAmostrasPorSinal * sinais.Count);
            geradorDeDadosDoAlgoritmo.Entradas.Should().HaveSameCount(geradorDeDadosDoAlgoritmo.Saidas);
            geradorDeDadosDoAlgoritmo.Saidas.Should().ContainInOrder(SaidasEsperadas(sinais.Count, quantidadeDeAmostrasPorSinal));

            int indiceDaEntrada = 0;
            var entradasEsperadas = EntradasEsperadasParaOsSinais(sinais);
            foreach (var input in geradorDeDadosDoAlgoritmo.Entradas)
            {
                input.Should().ContainInOrder(entradasEsperadas[indiceDaEntrada]);
                indiceDaEntrada++;
            }
        }

        private void DeveTerUmDadoDeTreinamento(GeradorDeDadosDeSinaisEstaticos geradorDeDadosDoAlgoritmo)
        {
            var arrayDaAmostra = amostraPadrao[0].MontarArrayEsperado();
            geradorDeDadosDoAlgoritmo.QuantidadeDeClasses.Should().Be(1);
            geradorDeDadosDoAlgoritmo.Entradas.Should().HaveCount(1);
            geradorDeDadosDoAlgoritmo.Entradas[0].Should().ContainInOrder(arrayDaAmostra);
            geradorDeDadosDoAlgoritmo.Saidas.Should().HaveCount(1);
            geradorDeDadosDoAlgoritmo.Saidas[0].Should().Be(0);
        }

        private double[][] EntradasEsperadasParaOsSinais(ICollection<Sinal> sinais)
        {
            var entradasEsperadas = new LinkedList<double[]>();

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    entradasEsperadas.AddLast(amostra[0].MontarArrayEsperado());
                }
            }

            return entradasEsperadas.ToArray();
        }

        private IEnumerable SaidasEsperadas(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            int[] saidas = new int[quantidadeDeSinais * quantidadeDeAmostrasPorSinal];
            int indiceDoSinal = 0;

            int i = 0;
            while (i < saidas.Length)
            {
                saidas[i] = indiceDoSinal;

                i++;

                if (i % quantidadeDeAmostrasPorSinal == 0)
                {
                    indiceDoSinal++;
                }
            }

            return saidas;
        }
    }
}