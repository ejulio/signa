using System.Linq;

namespace Signa.Domain.Features
{
    public class Finger : IFeature
    {
        public double[] Direction { get; set; }
        public FingerType Type { get; set; }

        public double[] ToArray()
        {
            var type = new double[] { (int)Type };
            return type.Concat(Direction).ToArray();
        }

        public static Finger Empty()
        {
            return new Finger
            {
                Type = FingerType.Thumb,
                Direction = new[] { 0.0, 0.0, 0.0 }
            };
        }
    }
}