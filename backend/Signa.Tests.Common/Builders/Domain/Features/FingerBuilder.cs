using Signa.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Features
{
    public class FingerBuilder
    {
        private double[] tipDirection;
        private FingerType type;

        public FingerBuilder WithDirection(double[] tipDirection)
        {
            this.tipDirection = tipDirection;
            return this;
        }

        public FingerBuilder OfType(FingerType type)
        {
            this.type = type;
            return this;
        }

        public Finger Build()
        {
            return new Finger
            {
                TipDirection = tipDirection,
                Type = type
            };
        }

        public static Finger Thumb()
        {
            return new FingerBuilder()
                .WithDirection(TipDirection())
                .OfType(FingerType.Thumb)
                .Build();
        }

        public static Finger Index()
        {
            return new FingerBuilder()
                .WithDirection(TipDirection())
                .OfType(FingerType.Index)
                .Build();
        }

        public static Finger Middle()
        {
            return new FingerBuilder()
                .WithDirection(TipDirection())
                .OfType(FingerType.Middle)
                .Build();
        }

        public static Finger Ring()
        {
            return new FingerBuilder()
                .WithDirection(TipDirection())
                .OfType(FingerType.Ring)
                .Build();
        }

        public static Finger Pinky()
        {
            return new FingerBuilder()
                .WithDirection(TipDirection())
                .OfType(FingerType.Pinky)
                .Build();
        }

        private static double[] TipDirection()
        {
            return new DoubleArrayBuilder().WithSize(3).Build();
        }

        public static Finger[] DefaultFingers()
        {
            return new[] 
            { 
                FingerBuilder.Thumb(), 
                FingerBuilder.Index(), 
                FingerBuilder.Middle(), 
                FingerBuilder.Ring(), 
                FingerBuilder.Pinky() 
            };
        }
    }
}
