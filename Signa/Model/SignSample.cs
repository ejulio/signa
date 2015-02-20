using System.Collections.Generic;
using System.Linq;

namespace Signa.Model
{
    public class SignSample
    {
        public HandSample[] Hands { get; set; }

        public double[] ToArray()
        {
            IEnumerable<double> handSamples = new double[0];
            foreach (var hand in Hands)
            {
                handSamples = handSamples.Concat(hand.ToArray());
            }

            return handSamples.ToArray();
        }
    }
}