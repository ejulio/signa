using System.Linq;

namespace Signa.Domain.Features
{
    public class Finger
    {
        public double[] TipDirection { get; set; }
        public FingerType Type { get; set; }

        public double[] ToArray()
        {
            var type = new double[] { (int)Type };
            return type.Concat(TipDirection).ToArray();
        }

        public static Finger Empty()
        {
            return new Finger
            {
                Type = FingerType.Thumb,
                TipDirection = new[] { 0.0, 0.0, 0.0 }
            };
        }
    }
}