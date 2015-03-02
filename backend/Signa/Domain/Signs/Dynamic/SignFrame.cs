using Signa.Domain.Features;
using System.Linq;

namespace Signa.Domain.Signs.Dynamic
{
    public class SignFrame
    {
        // TODO: Adicionar o tipo do frame: Gesto Estático, Frame Inicial, Frame Final
        private Hand leftHand;
        public Hand LeftHand
        {
            get { return leftHand; }
            set
            {
                leftHand = value ?? Hand.Empty();
            }
        }

        private Hand rightHand;
        public Hand RightHand
        {
            get { return rightHand; }
            set
            {
                rightHand = value ?? Hand.Empty();
            }
        }

        public double[] ToArray()
        {
            return LeftHand.ToArray()
                .Concat(RightHand.ToArray())
                .ToArray();
        }
    }
}