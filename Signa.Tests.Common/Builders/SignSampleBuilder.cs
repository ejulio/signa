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
        private HandSample[] hands;

        public SignSampleBuilder WithHands(HandSample[] hands)
        {
            this.hands = hands;
            return this;
        }

        public SignSample Build()
        {
            return new SignSample
            {
                Hands = hands
            };
        }
    }
}
