using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Tests.Common.Builders;
using FluentAssertions;

namespace Signa.Tests.Model
{
    [TestClass]
    public class SignSampleTest
    {
        [TestMethod]
        public void building_a_sample_with_one_hand()
        {
            var hands = new[] { new HandSampleBuilder().Build() };
            var signSample = new SignSampleBuilder()
                .WithHands(hands)
                .Build();

            var handSampleData = hands[0].ToArray();

            var sampleArray = signSample.ToArray();

            sampleArray.Should().HaveCount(handSampleData.Length);
            sampleArray.Should().ContainInOrder(handSampleData);
        }

        [TestMethod]
        public void building_a_sample_with_two_hands()
        {
            var hands = new[] { new HandSampleBuilder().Build(), new HandSampleBuilder().Build() };
            var signSample = new SignSampleBuilder()
                .WithHands(hands)
                .Build();

            var handSampleData1 = hands[0].ToArray();
            var handSampleData2 = hands[0].ToArray();

            var sampleArray = signSample.ToArray();

            sampleArray.Should().HaveCount(handSampleData1.Length + handSampleData2.Length);
            sampleArray.Should().ContainInOrder(handSampleData1);
            sampleArray.Should().ContainInOrder(handSampleData2);
        }
    }
}
