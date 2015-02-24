using System.Collections.Generic;
using System.Linq;

namespace Signa.Model
{
    public class SignSample
    {
        private HandSample leftHand;
        public HandSample LeftHand 
        {
            get { return leftHand; }
            set
            {
                leftHand = value ?? HandSample.DefaultSample();
            } 
        }

        private HandSample rightHand;
        public HandSample RightHand 
        {
            get { return rightHand; } 
            set
            {
                rightHand = value ?? HandSample.DefaultSample();
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