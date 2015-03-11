using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Sinais;
using System;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Util;

namespace Testes.Unidade.Dominio.Algoritmos.Dinamico
{
    [TestClass]
    public class GeradorDeCaracteristicaDeSinalDinamicoTeste
    {
        [TestMethod]
        public void criando_uma_amostra_de_sinal_dinamico()
        {
            var numeroAleatorio = new Random().Next(2, 20);
            var frames = DadoUmArrayDeFramesComQuantidade(numeroAleatorio);

            var geradorDeAmostraDeSinalDinamico = new GeradorDeCaracteristicasDeSinalDinamico();
            var arrayDeAmostras = geradorDeAmostraDeSinalDinamico.ExtrairCaracteristicasDaAmostra(frames);

            DeveTerRetornadoUmArrayComDadosDosFrames(frames, arrayDeAmostras);
        }

        private Frame[] DadoUmArrayDeFramesComQuantidade(int quantidadeDeFrames)
        {
            var frames = new Frame[quantidadeDeFrames];

            for (var i = 0; i < quantidadeDeFrames; i++)
            {
                frames[i] = new FrameBuilder().Construir();
            }

            return frames;
        }

        private void DeveTerRetornadoUmArrayComDadosDosFrames(Frame[] frames, double[][] arrayDeAmostras)
        {
            var dadosDosFrames = frames.Select(f => f.MontarArrayEsperado());

            var dadosEsperadosDoFrame = dadosDosFrames.ToArray();
            arrayDeAmostras.Should().HaveCount(dadosEsperadosDoFrame.Count());

            for (var i = 0; i < dadosEsperadosDoFrame.Count(); i++)
            {
                arrayDeAmostras[i].Should().HaveSameCount(dadosEsperadosDoFrame.ElementAt(i));
                arrayDeAmostras[i].Should().ContainInOrder(dadosEsperadosDoFrame.ElementAt(i));
            }
        }
    }
}