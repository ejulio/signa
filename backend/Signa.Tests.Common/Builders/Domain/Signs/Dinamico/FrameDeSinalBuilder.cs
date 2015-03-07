using Signa.Domain.Features;
using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Signs.Dynamic
{
    public class FrameDeSinalBuilder
    {
        private Hand leftHand;
        private Hand rightHand;

        public FrameDeSinalBuilder WithLeftHand(Hand leftHand)
        {
            this.leftHand = leftHand;
            return this;
        }

        public FrameDeSinalBuilder WithRightHand(Hand rightHand)
        {
            this.rightHand = rightHand;
            return this;
        }

        public FrameDeSinalBuilder WithDefaultLeftAndRightHand()
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
