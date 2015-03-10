using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using Signa.Dominio.Sinais.Caracteristicas;
using Signa.Util;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Dinamico
{
    [TestClass]
    public class GeradorDeAmostraDeSinalDinamicoTeste
    {
        [TestMethod]
        public void criando_uma_amostra_de_sinal_dinamico()
        {
            var numeroAleatorio = new Random().Next(2, 20);
            var frames = DadoUmArrayDeFramesComQuantidade(numeroAleatorio);

            var geradorDeAmostraDeSinalDinamico = new GeradorDeAmostraDeSinalDinamico();
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
            var dadosDosFrames = frames.Select(MontarArrayEsperadoParaOFrame);

            var dadosEsperadosDoFrame = dadosDosFrames.ToArray();
            arrayDeAmostras.Should().HaveCount(dadosEsperadosDoFrame.Count());

            for (var i = 0; i < dadosEsperadosDoFrame.Count(); i++)
            {
                arrayDeAmostras[i].Should().HaveSameCount(dadosEsperadosDoFrame.ElementAt(i));
                arrayDeAmostras[i].Should().ContainInOrder(dadosEsperadosDoFrame.ElementAt(i));
            }
        }

        private double[] MontarArrayEsperadoParaOFrame(Frame frame)
        {
            return MontarArrayEsperadoParaAMao(frame.MaoEsquerda)
                .Concat(MontarArrayEsperadoParaAMao(frame.MaoDireita))
                .ToArray();
        }

        private double[] MontarArrayEsperadoParaAMao(Mao mao)
        {
            var dadosDosDedos = mao.Dedos.Select(d =>
            {
                var tipo = new double[] { (int)d.Tipo };
                return tipo.Concat(d.Direcao).ToArray();
            }).Concatenar();

            return mao.VetorNormalDaPalma
                    .Concat(mao.DirecaoDaMao)
                    .Concat(dadosDosDedos)
                    .ToArray();
        }
    }
}