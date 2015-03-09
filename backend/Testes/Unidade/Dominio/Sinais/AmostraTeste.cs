using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Sinais;
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
            throw new NotImplementedException("Implementar uma amostra com vários frames apenas com a mão esquerda");
        }

        [TestMethod]
        public void criando_uma_amostra_apenas_com_a_mao_direita()
        {
            throw new NotImplementedException("Implementar uma amostra com vários frames apenas com a mão direita");
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
