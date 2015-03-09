using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Caracteristicas;
using Signa.Dominio.Sinais;
using System;
using System.Linq;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Sinais
{
    [TestClass]
    public class AmostraTeste
    {
        [TestMethod]
        public void criando_uma_amostra_de_sinal_estatico()
        {
            var frames = DadoUmArrayDeFramesComQuantidade(1);
            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .ConstruirAmostraEstatica();

            var arrayDeAmostras = amostra.ParaArray();

            DeveTerRetornadoUmArrayComDadosDoFrame(frames[0], arrayDeAmostras);
        }

        [TestMethod]
        public void criando_uma_amostra_de_sinal_dinamico()
        {
            var frames = DadoUmArrayDeFramesComQuantidade(2);
            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .ConstruirAmostraDinamica();

            var arrayDeAmostras = amostra.ParaArray();

            DeveTerRetornadoUmArrayComDadosDosFrames(frames, arrayDeAmostras);
        }

        [TestMethod]
        public void criando_uma_amostra_de_sinal_dinamico_com_um_numero_aleatorio_de_frames()
        {
            var quantidadeDeFrames = new Random().Next(3, 15);
            var frames = DadoUmArrayDeFramesComQuantidade(quantidadeDeFrames);
            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .ConstruirAmostraDinamica();

            var arrayDeAmostras = amostra.ParaArray();

            DeveTerRetornadoUmArrayComDadosDosFrames(frames, arrayDeAmostras);
        }


        [TestMethod]
        public void criando_uma_amostra_apenas_com_a_mao_esquerda()
        {
            var maoEsquerda = new MaoBuilder().Construir();
            var frames = DadoUmArrayDeFramesComQuantidadeComAsMaos(1, maoEsquerda, null);

            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .ConstruirAmostraEstatica();

            var arrayDeAmostras = amostra.ParaArray();

            DeveTerRetornadoUmArrayComDadosDoFrame(frames[0], arrayDeAmostras);
        }

        [TestMethod]
        public void criando_uma_amostra_apenas_com_a_mao_direita()
        {
            var maoDireita = new MaoBuilder().Construir();
            var frames = DadoUmArrayDeFramesComQuantidadeComAsMaos(3, null, maoDireita);

            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .ConstruirAmostraDinamica();

            var arrayDeAmostras = amostra.ParaArray();

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

        private Frame[] DadoUmArrayDeFramesComQuantidadeComAsMaos(int quantidadeDeFrames, Mao maoEsquerda, Mao maoDireita)
        {
            var frames = new Frame[quantidadeDeFrames];

            for (var i = 0; i < quantidadeDeFrames; i++)
            {
                frames[i] = new FrameBuilder()
                    .ComMaoEsquerda(maoEsquerda)
                    .ComMaoDireita(maoDireita)
                    .Construir();
            }

            return frames;
        }

        private void DeveTerRetornadoUmArrayComDadosDosFrames(Frame[] frames, double[][] arrayDeAmostras)
        {
            var dadosDosFrames = frames.Select(f => f.ToArray());

            var dadosEsperadosDoFrame = dadosDosFrames.ToArray();
            arrayDeAmostras.Should().HaveCount(dadosEsperadosDoFrame.Count());

            for (var i = 0; i < dadosEsperadosDoFrame.Count(); i++)
            {
                arrayDeAmostras[i].Should().HaveSameCount(dadosEsperadosDoFrame.ElementAt(i));
                arrayDeAmostras[i].Should().ContainInOrder(dadosEsperadosDoFrame.ElementAt(i));
            }
        }

        private void DeveTerRetornadoUmArrayComDadosDoFrame(Frame frame, double[] arrayDeAmostras)
        {
            var dadosEsperadosDoFrame = frame.ToArray();
            arrayDeAmostras.Should().HaveCount(dadosEsperadosDoFrame.Count());

            for (var i = 0; i < dadosEsperadosDoFrame.Count(); i++)
            {
                arrayDeAmostras[i].Should().Be(dadosEsperadosDoFrame.ElementAt(i));
                arrayDeAmostras[i].Should().Be(dadosEsperadosDoFrame.ElementAt(i));
            }
        }
    }
}
