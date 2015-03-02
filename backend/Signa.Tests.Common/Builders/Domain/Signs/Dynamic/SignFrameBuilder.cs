using Signa.Domain.Features;
using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Signs.Dynamic
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
