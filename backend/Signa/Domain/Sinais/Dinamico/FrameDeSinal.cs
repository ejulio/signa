using Signa.Domain.Caracteristicas;
using System.Linq;

namespace Signa.Domain.Sinais.Dinamico
{
    public class FrameDeSinal
    {
        // TODO: Adicionar o tipo do frame: Gesto Estático, Frame Inicial, Frame Final
        private Mao leftMao;
        public Mao LeftMao
        {
            get { return leftMao; }
            set
            {
                leftMao = value ?? Mao.Empty();
            }
        }

        private Mao rightMao;
        public Mao RightMao
        {
            get { return rightMao; }
            set
            {
                rightMao = value ?? Mao.Empty();
            }
        }

        public double[] ToArray()
        {
            return LeftMao.ToArray()
                .Concat(RightMao.ToArray())
                .ToArray();
        }
    }
}