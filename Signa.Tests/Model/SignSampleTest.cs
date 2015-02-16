using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Model;

namespace Signa.Tests.Model
{
    [TestClass]
    public class SignSampleTest
    {
        [TestMethod]
        public void returning_an_array_with_samples_properties()
        {
            double[] palmNormal = { 0.123, 0.556, 0.897 };
            double[] handDirection = { 0.896, 0.132, 0.745 };
            double[] anglesBetweenFingers = { 25, 10, 40, 50 };

            var signSample = new SignSample
            {
                PalmNormal = palmNormal,
                HandDirection = handDirection,
                AnglesBetweenFingers = anglesBetweenFingers
            };

            var signSampleArray = signSample.ToArray();

            signSampleArray.Should().Contain(palmNormal);
            signSampleArray.Should().Contain(handDirection);
            signSampleArray.Should().Contain(anglesBetweenFingers);
            signSampleArray.Length.Should().Be(10);
        }
    }
}
