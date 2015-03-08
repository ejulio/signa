using Signa.Util;
using System.Linq;

namespace Signa.Domain.Features
{
    public class Mao : IFeature
    {
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }

        public Dedo[] Dedos { get; set; }

        public double[] ToArray()
        {
            var fingersData = Dedos.Select(f => f.ToArray()).Concatenate();
            
            return PalmNormal
                .Concat(HandDirection)
                .Concat(fingersData)
                .ToArray();
        }
        public static Mao Empty()
        {
            return new Mao
            {
                Dedos = new [] { Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty() },
                HandDirection = new[] { 0.0, 0.0, 0.0 },
                PalmNormal = new[] { 0.0, 0.0, 0.0 }
            };
        }

    }
}