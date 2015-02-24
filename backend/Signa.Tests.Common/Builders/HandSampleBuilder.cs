using Signa.Model;

namespace Signa.Tests.Common.Builders
{
    public class HandSampleBuilder
    {
        private double[] anglesBetweenFingers;
        private double[] palmNormal;
        private double[] handDirection;

        public HandSampleBuilder()
        {
            anglesBetweenFingers = new DoubleArrayBuilder().WithSize(4).Build();
            palmNormal = new DoubleArrayBuilder().WithSize(3).Build();
            handDirection = new DoubleArrayBuilder().WithSize(3).Build();
        }

        public HandSampleBuilder WithAnglesBetweenFingers(double[] anglesBetweenFingers)
        {
            this.anglesBetweenFingers = anglesBetweenFingers;
            return this;
        }

        public HandSampleBuilder WithPalmNormal(double[] palmNormal)
        {
            this.palmNormal = palmNormal;
            return this;
        }

        public HandSampleBuilder WithHandDirection(double[] handDirection)
        {
            this.handDirection = handDirection;
            return this;
        }

        public HandSample Build()
        {
            return new HandSample
            {
                AnglesBetweenFingers = anglesBetweenFingers,
                PalmNormal = palmNormal,
                HandDirection = handDirection
            };
        }
    }
}
