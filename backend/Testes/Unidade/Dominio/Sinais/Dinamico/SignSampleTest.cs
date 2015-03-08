using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Sinais.Dinamico
{
    [TestClass]
    public class SignSampleTest
    {
        [TestMethod]
        public void building_a_sample_with_one_frame()
        {
            var frames = GivenAnArrayOfSignFramesWithCount(1);
            var signSample = new AmostraBuilder()
                .ComFrames(frames)
                .Construir();

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithFrameData(frames, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_two_frames()
        {
            var frames = GivenAnArrayOfSignFramesWithCount(2);
            var signSample = new AmostraBuilder()
                .ComFrames(frames)
                .Construir();

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithFrameData(frames, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_with_random_number_of_frames()
        {
            var numberOfFrames = new Random().Next(3, 15);
            var frames = GivenAnArrayOfSignFramesWithCount(numberOfFrames);
            var signSample = new AmostraBuilder()
                .ComFrames(frames)
                .Construir();

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithFrameData(frames, sampleArray);
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

        private Frame[] GivenAnArrayOfSignFramesWithCount(int count)
        {
            var frames = new Frame[count];

            for (var i = 0; i < count; i++)
            {
                frames[i] = new FrameBuilder().Construir();
            }

            return frames;
        }

        private void MustReturnAnArrayWithFrameData(Frame[] framesDeSinal, double[][] sampleArray)
        {
            var expectedFrameData = framesDeSinal.Select(f => f.ToArray());

            sampleArray.Should().HaveCount(expectedFrameData.Count());

            for (var i = 0; i < expectedFrameData.Count(); i++)
            {
                sampleArray[i].Should().HaveSameCount(expectedFrameData.ElementAt(i));
                sampleArray[i].Should().ContainInOrder(expectedFrameData.ElementAt(i));
            }
        }
    }
}
