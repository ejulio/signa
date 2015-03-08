using Signa.Dominio.Caracteristicas;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Caracteristicas;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class FrameBuilder
    {
        private Mao maoEsquerda;
        private Mao maoDireita;

        public FrameBuilder ComMaoEsquerda(Mao leftMao)
        {
            this.maoEsquerda = leftMao;
            return this;
        }

        public FrameBuilder WithRightHand(Mao rightMao)
        {
            this.maoDireita = rightMao;
            return this;
        }

        public FrameBuilder ComMaosEsquerdaEDireitaPadroes()
        {
            maoDireita = new MaoBuilder().Construir();
            maoEsquerda = new MaoBuilder().Construir();
            return this;
        }

        public FrameBuilder ParaOIndice(int indice)
        {
            maoDireita = new MaoBuilder().ParaOIndice(indice).Construir();
            maoEsquerda = new MaoBuilder().ParaOIndice(indice).Construir();
            return this;
        }

        public Frame Construir()
        {
            return new Frame
            {
                LeftMao = maoEsquerda,
                RightMao = maoDireita
            };
        }
    }
}
