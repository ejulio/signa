using Signa.Dominio.Caracteristicas;
using Signa.Dominio.Sinais.Estatico;
using Testes.Comum.Builders.Dominio.Caracteristicas;

namespace Testes.Comum.Builders.Dominio.Sinais.Estatico
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