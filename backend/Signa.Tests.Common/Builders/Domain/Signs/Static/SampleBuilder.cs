using Signa.Domain.Features;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Signs.Static
{
    public class SampleBuilder
    {
        private Hand leftHand;
        private Hand rightHand;

        public SampleBuilder WithLeftHand(Hand leftHand)
        {
            this.leftHand = leftHand;
            return this;
        }

        public SampleBuilder WithRightHand(Hand rightHand)
        {
            this.rightHand = rightHand;
            return this;
        }

        public SampleBuilder WithDefaultLeftAndRightHand()
        {
            rightHand = new HandBuilder().Build();
            leftHand = new HandBuilder().Build();
            return this;
        }

        public Sample Build()
        {
            return new Sample
            {
                LeftHand = leftHand,
                RightHand = rightHand
            };
        } 
    }
}