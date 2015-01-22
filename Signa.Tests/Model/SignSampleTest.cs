using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Model;
using FluentAssertions;

namespace Signa.Tests.Model
{
    [TestClass]
    public class SignSampleTest
    {
        [TestMethod]
        public void ToArray_Should_Return_Properties_In_The_Array()
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
