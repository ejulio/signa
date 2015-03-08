using System.Linq;
using Signa.Dominio.Caracteristicas;

namespace Signa.Dominio.Sinais
{
    public class Frame
    {
        // TODO: Adicionar o tipo do frame: Gesto Estático, Frame Inicial, Frame Final
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

        public double[] ToArray()
        {
            return MaoEsquerda.ToArray()
                .Concat(MaoDireita.ToArray())
                .ToArray();
        }
    }
}