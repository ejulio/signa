using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Model;
using Signa.Tests.Common.Builders;
using System.Linq;

namespace Signa.Tests.Model
{
    [TestClass]
    public class HandTest
    {
        [TestMethod]
        public void returning_an_array_with_samples_properties()
        {
            var palmNormal = new[] { 0.123, 0.556, 0.897 };
            var handDirection = new[] { 0.896, 0.132, 0.745 };
            var fingers = new[] 
            { 
                FingerBuilder.Thumb(), 
                FingerBuilder.Index(), 
                FingerBuilder.Middle(), 
                FingerBuilder.Ring(), 
                FingerBuilder.Pinky() 
            };

            var hand = new Hand
            {
                PalmNormal = palmNormal,
                HandDirection = handDirection,
                Fingers = fingers
            };

            var handData = hand.ToArray();

            var expectedHandData = GivenExpectedHandDataFor(palmNormal, handDirection, fingers);

            handData.Should().HaveSameCount(expectedHandData);
            handData.Should().ContainInOrder(expectedHandData);
        }

        private double[] GivenExpectedHandDataFor(double[] palmNormal, double[] handDirection, Finger[] fingers)
        {
            return palmNormal
                .Concat(handDirection)
                .Concat(fingers[0].ToArray())
                .Concat(fingers[1].ToArray())
                .Concat(fingers[2].ToArray())
                .Concat(fingers[3].ToArray())
                .Concat(fingers[4].ToArray())
                .ToArray();
        }
    }
}
