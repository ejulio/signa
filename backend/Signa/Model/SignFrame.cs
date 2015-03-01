using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Model
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