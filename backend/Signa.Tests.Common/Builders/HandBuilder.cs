using Signa.Model;

namespace Signa.Tests.Common.Builders
{
    public class HandBuilder
    {
        private Finger[] fingers;
        private double[] palmNormal;
        private double[] handDirection;

        public HandBuilder()
        {
            fingers = FingerBuilder.DefaultFingers();
            palmNormal = new DoubleArrayBuilder().WithSize(3).Build();
            handDirection = new DoubleArrayBuilder().WithSize(3).Build();
        }

        public HandBuilder WithFingers(Finger[] fingers)
        {
            this.fingers = fingers;
            return this;
        }

        public HandBuilder WithPalmNormal(double[] palmNormal)
        {
            this.palmNormal = palmNormal;
            return this;
        }

        public HandBuilder WithHandDirection(double[] handDirection)
        {
            this.handDirection = handDirection;
            return this;
        }

        public Hand Build()
        {
            return new Hand
            {
                Fingers = fingers,
                PalmNormal = palmNormal,
                HandDirection = handDirection
            };
        }
    }
}
