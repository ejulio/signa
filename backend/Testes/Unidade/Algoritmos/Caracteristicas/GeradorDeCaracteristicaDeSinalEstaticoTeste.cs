﻿using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using Dominio.Sinais.Caracteristicas;
using Dominio.Util;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Util;

namespace Testes.Unidade.Algoritmos.Caracteristicas
{
    [TestClass]
    public class GeradorDeCaracteristicaDeSinalEstaticoTeste
    {
        [TestMethod]
        public void extraindo_caracteristicas_de_um_frame()
        {
            var maoEsquerda = DadaUmaMaoEsquerda();
            var maoDireita = DadaUmaMaoDireita();
            var frame = DadoUmFrameComMaos(maoEsquerda, maoDireita);

            var geradorDeAmostraDeSinalEstatico = new GeradorDeCaracteristicasDeSinalEstatico();
            var frameArray = geradorDeAmostraDeSinalEstatico.ExtrairCaracteristicasDaAmostra(new[] { frame });

            DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(frame, frameArray);
        }

        private Frame DadoUmFrameComMaos(Mao maoEsquerda, Mao maoDireita)
        {
            var frame = new FrameBuilder()
                .ComMaoEsquerda(maoEsquerda)
                .ComMaoDireita(maoDireita)
                .Construir();

            return frame;
        }

        private Mao DadaUmaMaoDireita()
        {
            var maoDireita = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.4, 0.5, 0.6 })
                .ComVetorNormalDaPalma(new[] { 0.0, 1.0, 1.0 })
                .Construir();
            return maoDireita;
        }

        private Mao DadaUmaMaoEsquerda()
        {
            var maoEsquerda = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.1, 0.2, 0.3 })
                .ComVetorNormalDaPalma(new[] { 1.0, 0.0, 1.0 })
                .Construir();
            return maoEsquerda;
        }

        private void DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(Frame frame, double[] frameArray)
        {
            var dadosDoFrameEsperados = frame.MontarArrayEsperado();

            frameArray.Should().HaveCount(dadosDoFrameEsperados.Count());
            frameArray.Should().ContainInOrder(dadosDoFrameEsperados);
        }
    }
}