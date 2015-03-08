using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Features;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Features;
using Signa.Tests.Common.Builders.Domain.Signs.Static;
using System.Linq;

namespace Signa.Tests.Domain.Signs.Static
{
    [TestClass]
    public class SampleTest
    {
        [TestMethod]
        public void building_a_sample_with_left_hand()
        {
            var leftHand = GivenLeftHand();
            var rightHand = Mao.Empty();
            var sample = GivenSampleWithHands(leftHand, null);

            var sampleArray = sample.ToArray();

            MustReturnAnArrayWithLeftAndRightHandData(leftHand, rightHand, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_with_right_hand()
        {
            var leftHand = Mao.Empty();
            var rightHand = GivenRightHand();
            var sample = GivenSampleWithHands(null, rightHand);

            var sampleArray = sample.ToArray();

            MustReturnAnArrayWithLeftAndRightHandData(leftHand, rightHand, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_with_two_hands()
        {
            var leftHand = GivenLeftHand();
            var rightHand = GivenRightHand();
            var sample = GivenSampleWithHands(leftHand, rightHand);

            var sampleArray = sample.ToArray();

            MustReturnAnArrayWithLeftAndRightHandData(leftHand, rightHand, sampleArray);
        }

        [TestMethod]
        public void when_null_right_and_left_hand_should_have_default_values()
        {
            var sample = new Sample
            {
                LeftMao = null,
                RightMao = null
            };

            var defaultValues = Mao.Empty();

            sample.LeftMao.ToArray().Should().ContainInOrder(defaultValues.ToArray());
            sample.RightMao.ToArray().Should().ContainInOrder(defaultValues.ToArray());
        }

        private Sample GivenSampleWithHands(Mao leftMao, Mao rightMao)
        {
            var sample = new AmostraBuilder()
                .WithLeftHand(leftMao)
                .WithRightHand(rightMao)
                .Construir();

            return sample;
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

        private void MustReturnAnArrayWithLeftAndRightHandData(Mao leftMao, Mao rightMao, double[] frameArray)
        {
            var expectedFrameData = leftMao.ToArray().Concat(rightMao.ToArray()).ToArray();

            frameArray.Should().HaveCount(expectedFrameData.Count());
            frameArray.Should().ContainInOrder(expectedFrameData);
        }
    }
}