using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Tests.Common.Builders;
using FluentAssertions;
using Signa.Model;
using System.Linq;

namespace Signa.Tests.Model
{
    [TestClass]
    public class SignSampleTest
    {
        [TestMethod]
        public void building_a_sample_with_left_hand()
        {
            var leftHand = GivenLeftHand();
            var rightHand = HandSample.DefaultSample();
            var signSample = GivenSignSampleWithHands(leftHand, null);

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithLeftAndRightData(leftHand, rightHand, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_with_right_hand()
        {
            var leftHand = HandSample.DefaultSample();
            var rightHand = GivenRightHand();
            var signSample = GivenSignSampleWithHands(null, rightHand);

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithLeftAndRightData(leftHand, rightHand, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_with_two_hands()
        {
            var leftHand = GivenLeftHand();
            var rightHand = GivenRightHand();
            var signSample = GivenSignSampleWithHands(leftHand, rightHand);

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithLeftAndRightData(leftHand, rightHand, sampleArray);
        }

        [TestMethod]
        public void when_null_right_and_left_hand_should_have_default_values()
        {
            var signSample = new SignSample
            {
                LeftHand = null,
                RightHand = null
            };

            var defaultValues = HandSample.DefaultSample();

            signSample.LeftHand.ToArray().Should().ContainInOrder(defaultValues.ToArray());
            signSample.RightHand.ToArray().Should().ContainInOrder(defaultValues.ToArray());
        }

        private SignSample GivenSignSampleWithHands(HandSample leftHand, HandSample rightHand)
        {
            var signSample = new SignSampleBuilder()
                .WithLeftHand(leftHand)
                .WithRightHand(rightHand)
                .Build();

            return signSample;
        }

        private HandSample GivenRightHand()
        {
            var rightHand = new HandSampleBuilder()
                .WithAnglesBetweenFingers(new[] { 0.5, 0.5, 0.7, 0.7 })
                .WithHandDirection(new[] { 0.4, 0.5, 0.6 })
                .WithPalmNormal(new[] { 0.0, 1.0, 1.0 })
                .Build();
            return rightHand;
        }

        private HandSample GivenLeftHand()
        {
            var leftHand = new HandSampleBuilder()
                .WithAnglesBetweenFingers(new[] { 0.1, 0.1, 0.2, 0.2 })
                .WithHandDirection(new[] { 0.1, 0.2, 0.3 })
                .WithPalmNormal(new[] { 1.0, 0.0, 1.0 })
                .Build();
            return leftHand;
        }

        private void MustReturnAnArrayWithLeftAndRightData(HandSample leftHand, HandSample rightHand, double[] sampleArray)
        {
            var expectedSampleData = leftHand.ToArray().Concat(rightHand.ToArray());

            sampleArray.Should().HaveCount(expectedSampleData.Count());
            sampleArray.Should().ContainInOrder(expectedSampleData);
        }
    }
}
