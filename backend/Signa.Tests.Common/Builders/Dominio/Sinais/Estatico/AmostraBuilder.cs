using Signa.Domain.Caracteristicas;
using Signa.Domain.Sinais.Estatico;
using Signa.Tests.Common.Builders.Dominio.Caracteristicas;

namespace Signa.Tests.Common.Builders.Dominio.Sinais.Estatico
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