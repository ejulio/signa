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
                if (value == null)
                    value = HandSample.DefaultSample();

                leftHand = value;
            } 
        }

        private HandSample rightHand;
        public HandSample RightHand 
        {
            get { return rightHand; } 
            set
            {
                if (value == null)
                    value = HandSample.DefaultSample();

                rightHand = value;
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