using System.Linq;
using Signa.Util;

namespace Signa.Domain.Features
{
    public class Hand : IFeature
    {
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }

        public Finger[] Fingers { get; set; }

        public double[] ToArray()
        {
            var fingersData = Fingers.Select(f => f.ToArray()).Concatenate();
            
            return PalmNormal
                .Concat(HandDirection)
                .Concat(fingersData)
                .ToArray();
        }
        public static Hand Empty()
        {
            return new Hand
            {
                Fingers = new [] { Finger.Empty(), Finger.Empty(), Finger.Empty(), Finger.Empty(), Finger.Empty() },
                HandDirection = new[] { 0.0, 0.0, 0.0 },
                PalmNormal = new[] { 0.0, 0.0, 0.0 }
            };
        }

    }
}