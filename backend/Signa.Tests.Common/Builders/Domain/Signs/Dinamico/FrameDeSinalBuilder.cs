using Signa.Domain.Features;
using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Signs.Dinamico
{
    public class FrameDeSinalBuilder
    {
        private Mao maoEsquerda;
        private Mao maoDireita;

        public FrameDeSinalBuilder ComMaoEsquerda(Mao leftMao)
        {
            this.maoEsquerda = leftMao;
            return this;
        }

        public FrameDeSinalBuilder WithRightHand(Mao rightMao)
        {
            this.maoDireita = rightMao;
            return this;
        }

        public FrameDeSinalBuilder ComMaosEsquerdaEDireitaPadroes()
        {
            maoDireita = new MaoBuilder().Construir();
            maoEsquerda = new MaoBuilder().Construir();
            return this;
        }

        public FrameDeSinalBuilder ParaOIndice(int indice)
        {
            maoDireita = new MaoBuilder().ParaOIndice(indice).Construir();
            maoEsquerda = new MaoBuilder().ParaOIndice(indice).Construir();
            return this;
        }

        public FrameDeSinal Construir()
        {
            return new FrameDeSinal
            {
                LeftMao = maoEsquerda,
                RightMao = maoDireita
            };
        }
    }
}
