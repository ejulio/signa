using Signa.Model;

namespace Signa.Tests.Common.Builders
{
    public class SignSampleBuilder
    {
        private double[] anglesBetweenFingers;
        private double[] palmNormal;
        private double[] handDirection;

        public SignSampleBuilder()
        {
            anglesBetweenFingers = new DoubleArrayBuilder().WithSize(4).Build();
            palmNormal = new DoubleArrayBuilder().WithSize(3).Build();
            handDirection = new DoubleArrayBuilder().WithSize(3).Build();
        }

        public SignSampleBuilder WithAnglesBetweenFingers(double[] anglesBetweenFingers)
        {
            this.anglesBetweenFingers = anglesBetweenFingers;
            return this;
        }

        public SignSampleBuilder WithPalmNormal(double[] palmNormal)
        {
            this.palmNormal = palmNormal;
            return this;
        }

        public SignSampleBuilder WithHandDirection(double[] handDirection)
        {
            this.handDirection = handDirection;
            return this;
        }

        public SignSample Build()
        {
            return new SignSample
            {
                AnglesBetweenFingers = anglesBetweenFingers,
                PalmNormal = palmNormal,
                HandDirection = handDirection
            };
        }
    }
}
