using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Features;

namespace Signa.Tests.Domain.Features
{
    [TestClass]
    public class FingerTest
    {
        [TestMethod]
        public void returning_an_array_with_finger_data()
        {
            var finger = new Finger
            {
                Type = FingerType.Thumb,
                Direction = new[] { 0.12, 0.478, 0.6985 }
            };

            var fingerArray = finger.ToArray();

            var expectedFingerArray = new double[] { (int)FingerType.Thumb, 0.12, 0.478, 0.6985 };

            fingerArray.Should().HaveSameCount(expectedFingerArray);
            fingerArray.Should().ContainInOrder(expectedFingerArray);
        }
    }
}
