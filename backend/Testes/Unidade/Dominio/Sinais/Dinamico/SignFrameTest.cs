using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Caracteristicas;
using Signa.Dominio.Sinais.Dinamico;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais.Dinamico;

namespace Testes.Unidade.Dominio.Sinais.Dinamico
{
    [TestClass]
    public class SignFrameTest
    {
        [TestMethod]
        public void building_a_frame_with_left_hand()
        {
            var leftHand = GivenLeftHand();
            var rightHand = Mao.Empty();
            var signFrame = GivenSignFrameWithHands(leftHand, null);

            var frameArray = signFrame.ToArray();

            MustReturnAnArrayWithLeftAndRightData(leftHand, rightHand, frameArray);
        }

        [TestMethod]
        public void building_a_frame_with_right_hand()
        {
            var leftHand = Mao.Empty();
            var rightHand = GivenRightHand();
            var signFrame = GivenSignFrameWithHands(null, rightHand);

            var frameArray = signFrame.ToArray();

            MustReturnAnArrayWithLeftAndRightData(leftHand, rightHand, frameArray);
        }

        [TestMethod]
        public void building_a_frame_with_two_hands()
        {
            var leftHand = GivenLeftHand();
            var rightHand = GivenRightHand();
            var signFrame = GivenSignFrameWithHands(leftHand, rightHand);

            var frameArray = signFrame.ToArray();

            MustReturnAnArrayWithLeftAndRightData(leftHand, rightHand, frameArray);
        }

        [TestMethod]
        public void when_null_right_and_left_hand_should_have_default_values()
        {
            var signFrame = new FrameDeSinal
            {
                LeftMao = null,
                RightMao = null
            };

            var defaultValues = Mao.Empty();

            signFrame.LeftMao.ToArray().Should().ContainInOrder(defaultValues.ToArray());
            signFrame.RightMao.ToArray().Should().ContainInOrder(defaultValues.ToArray());
        }

        private FrameDeSinal GivenSignFrameWithHands(Mao leftMao, Mao rightMao)
        {
            var signFrame = new FrameDeSinalBuilder()
                .ComMaoEsquerda(leftMao)
                .WithRightHand(rightMao)
                .Construir();

            return signFrame;
        }

        private Mao GivenRightHand()
        {
            var rightHand = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.4, 0.5, 0.6 })
                .ComVetorNormalDaPalma(new[] { 0.0, 1.0, 1.0 })
                .Construir();
            return rightHand;
        }

        private Mao GivenLeftHand()
        {
            var leftHand = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.1, 0.2, 0.3 })
                .ComVetorNormalDaPalma(new[] { 1.0, 0.0, 1.0 })
                .Construir();
            return leftHand;
        }

        private void MustReturnAnArrayWithLeftAndRightData(Mao leftMao, Mao rightMao, double[] frameArray)
        {
            var expectedFrameData = leftMao.ToArray().Concat(rightMao.ToArray());

            frameArray.Should().HaveCount(expectedFrameData.Count());
            frameArray.Should().ContainInOrder(expectedFrameData);
        }
    }
}
