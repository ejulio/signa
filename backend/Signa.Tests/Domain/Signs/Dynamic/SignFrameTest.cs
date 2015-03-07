using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Features;
using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Features;
using Signa.Tests.Common.Builders.Domain.Signs.Dynamic;
using System.Linq;

namespace Signa.Tests.Domain.Signs.Dynamic
{
    [TestClass]
    public class SignFrameTest
    {
        [TestMethod]
        public void building_a_frame_with_left_hand()
        {
            var leftHand = GivenLeftHand();
            var rightHand = Hand.Empty();
            var signFrame = GivenSignFrameWithHands(leftHand, null);

            var frameArray = signFrame.ToArray();

            MustReturnAnArrayWithLeftAndRightData(leftHand, rightHand, frameArray);
        }

        [TestMethod]
        public void building_a_frame_with_right_hand()
        {
            var leftHand = Hand.Empty();
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
            var signFrame = new SignFrame
            {
                LeftHand = null,
                RightHand = null
            };

            var defaultValues = Hand.Empty();

            signFrame.LeftHand.ToArray().Should().ContainInOrder(defaultValues.ToArray());
            signFrame.RightHand.ToArray().Should().ContainInOrder(defaultValues.ToArray());
        }

        private SignFrame GivenSignFrameWithHands(Hand leftHand, Hand rightHand)
        {
            var signFrame = new FrameDeSinalBuilder()
                .WithLeftHand(leftHand)
                .WithRightHand(rightHand)
                .Build();

            return signFrame;
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

        private void MustReturnAnArrayWithLeftAndRightData(Hand leftHand, Hand rightHand, double[] frameArray)
        {
            var expectedFrameData = leftHand.ToArray().Concat(rightHand.ToArray());

            frameArray.Should().HaveCount(expectedFrameData.Count());
            frameArray.Should().ContainInOrder(expectedFrameData);
        }
    }
}
