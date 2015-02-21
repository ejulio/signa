using Signa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signa.Tests.Common.Builders
{
    public class SignSampleBuilder
    {
        private HandSample leftHand;
        private HandSample rightHand;

        public SignSampleBuilder WithLeftHand(HandSample leftHand)
        {
            this.leftHand = leftHand;
            return this;
        }

        public SignSampleBuilder WithRightHand(HandSample rightHand)
        {
            this.rightHand = rightHand;
            return this;
        }

        public SignSample Build()
        {
            return new SignSample
            {
                LeftHand = leftHand,
                RightHand = rightHand
            };
        }
    }
}
