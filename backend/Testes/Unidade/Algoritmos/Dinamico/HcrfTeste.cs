﻿using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dados;
using Dominio.Algoritmos.Dinamico;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Algoritmos.Dinamico
{
    [TestClass]
    public class HcrfTeste
    {
        [TestMethod]
        public void reconhecendo_sem_treinar_o_algoritmo()
        {
            var frames = new Frame[0];
            Action acao = () => new Hcrf(new GeradorDeCaracteristicasDeSinalDinamico()).Reconhecer(frames);

            acao.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int quantidadeDeSinais = 3;
            const int quantidadeDeAmostrasPorSinal = 4;
            const int indiceDoSinalResultante = 2;

            var hcrf = DadoUmAlgoritmoTreinado(quantidadeDeSinais, quantidadeDeAmostrasPorSinal);

            var framesParaReconhecer = new []
            {
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir()
            };

            int sinalReconhecido = hcrf.Reconhecer(framesParaReconhecer);

            sinalReconhecido.Should().Be(indiceDoSinalResultante);
        }

        private Hcrf DadoUmAlgoritmoTreinado(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var hcrf = new Hcrf(new GeradorDeCaracteristicasDeSinalDinamico());
            var colecaoDeSinais = new ColecaoDeSinaisBuilder()
                .ComQuantidadeDeSinais(quantidadeDeSinais)
                .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostrasPorSinal)
                .ComGeradorDeAmostras(i => new ColecaoDeFramesBuilder().ParaOIndiceComQuantidade(i, 5).Construir())
                .Construir();

            var dados = new GeradorDeDadosDeSinaisDinamicos(colecaoDeSinais);
            hcrf.Treinar(dados);

            return hcrf;
        }
    }
}