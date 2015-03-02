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
            var rightHand = Hand.Empty();
            var sample = GivenSampleWithHands(leftHand, null);

            var sampleArray = sample.ToArray();

            MustReturnAnArrayWithLeftAndRightHandData(leftHand, rightHand, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_with_right_hand()
        {
            var leftHand = Hand.Empty();
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
                LeftHand = null,
                RightHand = null
            };

            var defaultValues = Hand.Empty();

            sample.LeftHand.ToArray().Should().ContainInOrder(defaultValues.ToArray());
            sample.RightHand.ToArray().Should().ContainInOrder(defaultValues.ToArray());
        }

        private Sample GivenSampleWithHands(Hand leftHand, Hand rightHand)
        {
            var sample = new SampleBuilder()
                .WithLeftHand(leftHand)
                .WithRightHand(rightHand)
                .Build();

            return sample;
        }

        private Hand GivenRightHand()
        {
            var rightHand = new HandBuilder()
                .WithFingers(FingerBuilder.DefaultFingers())
                .WithHandDirection(new[] { 0.4, 0.5, 0.6 })
                .WithPalmNormal(new[] { 0.0, 1.0, 1.0 })
                .Build();

            return rightHand;
        }

        private Hand GivenLeftHand()
        {
            var leftHand = new HandBuilder()
                .WithFingers(FingerBuilder.DefaultFingers())
                .WithHandDirection(new[] { 0.1, 0.2, 0.3 })
                .WithPalmNormal(new[] { 1.0, 0.0, 1.0 })
                .Build();

            return leftHand;
        }

        private void MustReturnAnArrayWithLeftAndRightHandData(Hand leftHand, Hand rightHand, double[] frameArray)
        {
            var expectedFrameData = leftHand.ToArray().Concat(rightHand.ToArray()).ToArray();

            frameArray.Should().HaveCount(expectedFrameData.Count());
            frameArray.Should().ContainInOrder(expectedFrameData);
        }
    }
}