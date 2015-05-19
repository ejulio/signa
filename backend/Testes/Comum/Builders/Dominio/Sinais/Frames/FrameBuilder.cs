using Dominio.Sinais.Frames;
using Dominio.Sinais.Maos;
using Testes.Comum.Builders.Dominio.Sinais.Maos;

namespace Testes.Comum.Builders.Dominio.Sinais.Frames
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
