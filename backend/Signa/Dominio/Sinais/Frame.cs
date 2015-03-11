using Signa.Dominio.Sinais.Caracteristicas;

namespace Signa.Dominio.Sinais
{
    public class Frame
    {
        private Mao maoEsquerda;
        public Mao MaoEsquerda
        {
            get { return maoEsquerda; }
            set
            {
                maoEsquerda = value ?? Mao.Vazia();
            }
        }

        private Mao maoDireita;
        public Mao MaoDireita
        {
            get { return maoDireita; }
            set
            {
                maoDireita = value ?? Mao.Vazia();
            }
        }
    }
}