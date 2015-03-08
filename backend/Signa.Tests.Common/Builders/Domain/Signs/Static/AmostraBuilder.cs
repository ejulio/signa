using Signa.Domain.Features;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Signs.Static
{
    public class AmostraBuilder
    {
        private Mao leftMao;
        private Mao rightMao;

        public AmostraBuilder WithLeftHand(Mao leftMao)
        {
            this.leftMao = leftMao;
            return this;
        }

        public AmostraBuilder WithRightHand(Mao rightMao)
        {
            this.rightMao = rightMao;
            return this;
        }

        public AmostraBuilder WithDefaultLeftAndRightHand()
        {
            rightMao = new MaoBuilder().Construir();
            leftMao = new MaoBuilder().Construir();
            return this;
        }

        public Sample Construir()
        {
            return new Sample
            {
                LeftMao = leftMao,
                RightMao = rightMao
            };
        } 
    }
}