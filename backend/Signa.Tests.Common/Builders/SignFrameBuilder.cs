using Signa.Model;

namespace Signa.Tests.Common.Builders
{
    public class SignFrameBuilder
    {
        private Hand leftHand;
        private Hand rightHand;

        public SignFrameBuilder WithLeftHand(Hand leftHand)
        {
            this.leftHand = leftHand;
            return this;
        }

        public SignFrameBuilder WithRightHand(Hand rightHand)
        {
            this.rightHand = rightHand;
            return this;
        }

        public SignFrameBuilder WithDefaultLeftAndRightHand()
        {
            rightHand = new HandBuilder().Build();
            leftHand = new HandBuilder().Build();
            return this;
        }

        public SignFrame Build()
        {
            return new SignFrame
            {
                LeftHand = leftHand,
                RightHand = rightHand
            };
        }
    }
}
