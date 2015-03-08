﻿using Signa.Domain.Features;
using System.Linq;

namespace Signa.Domain.Signs.Static
{
    public class Sample
    {
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