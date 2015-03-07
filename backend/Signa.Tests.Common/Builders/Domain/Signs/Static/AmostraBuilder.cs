using Signa.Domain.Features;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Signs.Static
{
    public class AmostraBuilder
    {
        private Hand leftHand;
        private Hand rightHand;

        public AmostraBuilder WithLeftHand(Hand leftHand)
        {
            this.leftHand = leftHand;
            return this;
        }

        public AmostraBuilder WithRightHand(Hand rightHand)
        {
            this.rightHand = rightHand;
            return this;
        }

        public AmostraBuilder WithDefaultLeftAndRightHand()
        {
            rightHand = new HandBuilder().Build();
            leftHand = new HandBuilder().Build();
            return this;
        }

        public Sample Construir()
        {
            return new Sample
            {
                LeftHand = leftHand,
                RightHand = rightHand
            };
        } 
    }
}