using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Sinais.Dinamico
{
    [TestClass]
    public class AmostraTeste
    {
        [TestMethod]
        public void criando_uma_amostra_com_um_frame()
        {
            var frames = DadoUmArrayDeFrameComQuantidade(1);
            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .Construir();

            var arrayDeAmostras = amostra.ToArray();

            DeveTerRetornadoUmArrayComDadosDoFrame(frames, arrayDeAmostras);
        }

        [TestMethod]
        public void criando_uma_amostra_com_dois_frames()
        {
            var frames = DadoUmArrayDeFrameComQuantidade(2);
            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .Construir();

            var arrayDeAmostras = amostra.ToArray();

            DeveTerRetornadoUmArrayComDadosDoFrame(frames, arrayDeAmostras);
        }

        [TestMethod]
        public void criando_uma_amostra_com_um_numero_aleatorio_de_frames()
        {
            var quantidadeDeFrames = new Random().Next(3, 15);
            var frames = DadoUmArrayDeFrameComQuantidade(quantidadeDeFrames);
            var amostra = new AmostraBuilder()
                .ComFrames(frames)
                .Construir();

            var arrayDeAmostras = amostra.ToArray();

            DeveTerRetornadoUmArrayComDadosDoFrame(frames, arrayDeAmostras);
        }

        [TestMethod]
        public void criando_uma_amostra_de_sinal_estatico()
        {
            throw new NotImplementedException("Implementar uma amostra com apenas um frame");
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

        private Frame[] DadoUmArrayDeFrameComQuantidade(int quantidadeDeFrames)
        {
            var frames = new Frame[quantidadeDeFrames];

            for (var i = 0; i < quantidadeDeFrames; i++)
            {
                frames[i] = new FrameBuilder().Construir();
            }

            return frames;
        }

        private void DeveTerRetornadoUmArrayComDadosDoFrame(Frame[] frames, double[][] arrayDeAmostras)
        {
            var expectedFrameData = frames.Select(f => f.ToArray());

            arrayDeAmostras.Should().HaveCount(expectedFrameData.Count());

            for (var i = 0; i < expectedFrameData.Count(); i++)
            {
                arrayDeAmostras[i].Should().HaveSameCount(expectedFrameData.ElementAt(i));
                arrayDeAmostras[i].Should().ContainInOrder(expectedFrameData.ElementAt(i));
            }
        }
    }
}
