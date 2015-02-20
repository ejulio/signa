using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Model;
using Signa.Recognizer;
using Signa.Tests.Common.Builders;
using System;
using System.Collections.Generic;

namespace Signa.Tests.Recognizer
{
    [TestClass]
    public class SvmTest
    {
        [TestMethod]
        public void recognizing_without_trainning_throw_an_error()
        {
            var sample = new SignSampleBuilder().Build();
            Action recognizeCall = () => Svm.Instance.Recognize(sample);

            recognizeCall.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void recognizing_an_input()
        {
            const int signCount = 3;
            const int samplesPerSign = 2;
            const int signResultIndex = 2;

            GivenATrainedAlgorithm(signCount, samplesPerSign);
            
            var result = Svm.Instance.Recognize(BuildSignSampleByIndex(signResultIndex));

            result.Should().Be(signResultIndex);
        }

        private static void GivenATrainedAlgorithm(int signCount, int samplesPerSign)
        {
            var signs = GivenACollectionOfSigns(signCount, samplesPerSign);
            var trainningData = new SvmTrainningData(signs);
            Svm.Instance.Train(trainningData);
        }

        private static ICollection<Sign> GivenACollectionOfSigns(int signCount, int samplesPerSign)
        {
            var signs = new SignCollectionBuilder()
                            .WithSize(signCount)            
                            .WithSampleCount(samplesPerSign)
                            .WithSampleGenerator(i => BuildSignSampleByIndex(i))
                            .Build();
            return signs;
        }

        private static SignSample BuildSignSampleByIndex(int index)
        {
            var hands = new[] 
            {
                new HandSampleBuilder()
                    .WithAnglesBetweenFingers(new double[] { index, index, index, index })
                    .WithPalmNormal(new double[] { index, index, index })
                    .WithHandDirection(new double[] { index, index, index })
                    .Build(),

                new HandSampleBuilder()
                    .WithAnglesBetweenFingers(new double[] { index, index, index, index })
                    .WithPalmNormal(new double[] { index, index, index })
                    .WithHandDirection(new double[] { index, index, index })
                    .Build()
            };
            return new SignSampleBuilder()
                .WithHands(hands)
                .Build();
        }
    }
}
