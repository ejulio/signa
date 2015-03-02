using System.Linq;
using Signa.Domain.Features;

namespace Signa.Domain.Signs.Static
{
    public class Sample
    {
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