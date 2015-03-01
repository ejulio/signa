using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Model
{
    public class Hand
    {
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }

        public Finger[] Fingers { get; set; }

        public double[] ToArray()
        {
            var fingersData = Fingers[0].ToArray().Concat(Fingers[1].ToArray()).Concat(Fingers[2].ToArray()).Concat(Fingers[3].ToArray()).Concat(Fingers[4].ToArray());
            
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