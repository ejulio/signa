using Signa.Dominio.Sinais;
using Signa.Dominio.Sinais.Caracteristicas;
using Testes.Comum.Builders.Dominio.Caracteristicas;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class FrameBuilder
    {
        private Mao maoEsquerda = new MaoBuilder().Construir();
        private Mao maoDireita = new MaoBuilder().Construir();

        public FrameBuilder ComMaoEsquerda(Mao leftMao)
        {
            this.maoEsquerda = leftMao;
            return this;
        }

        public FrameBuilder ComMaoDireita(Mao rightMao)
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
                MaoEsquerda = maoEsquerda,
                MaoDireita = maoDireita
            };
        }
    }
}
