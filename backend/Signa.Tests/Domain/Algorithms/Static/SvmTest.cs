using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Algorithms.Static;
using Signa.Domain.Features;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Features;
using Signa.Tests.Common.Builders.Domain.Signs;
using Signa.Tests.Common.Builders.Domain.Signs.Static;
using System;
using System.Collections.Generic;

namespace Signa.Tests.Domain.Algorithms.Static
{
    [TestClass]
    public class SvmTest
    {
        [TestMethod]
        public void recognizing_without_trainning_throw_an_error()
        {
            var sample = new SampleBuilder().Build();
            Action recognizeCall = () => new Svm().Recognize(sample);

            recognizeCall.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void recognizing_an_input()
        {
            const int signCount = 3;
            const int samplesPerSign = 2;
            const int signResultIndex = 2;

            var svm = GivenATrainedAlgorithm(signCount, samplesPerSign);

            var sample = BuildSignSampleByIndex(signResultIndex);

            var result = svm.Recognize(sample);

            result.Should().Be(signResultIndex);
        }

        private Svm GivenATrainedAlgorithm(int signCount, int samplesPerSign)
        {
            var signs = GivenACollectionOfSigns(signCount, samplesPerSign);
            var trainningData = new SignRecognitionAlgorithmData(signs);

            var svm = new Svm();
            svm.Train(trainningData);

            return svm;
        }

        private ICollection<Sign> GivenACollectionOfSigns(int signCount, int samplesPerSign)
        {
            var signs = new StaticSignCollectionBuilder()
                            .WithSize(signCount)            
                            .WithSampleCount(samplesPerSign)
                            .WithSampleGenerator(BuildSignSampleByIndex)
                            .Build();
            return signs;
        }

        private Sample BuildSignSampleByIndex(int index)
        {
            var leftHand = GivenHandWithFingers(index);
            var rightHand = GivenHandWithFingers(index);

            return new SampleBuilder()
                .WithLeftHand(leftHand)
                .WithRightHand(rightHand)
                .Build();
        }

        private Hand GivenHandWithFingers(int index)
        {
            var fingers = new[] 
            {
                new FingerBuilder().OfType(FingerType.Thumb).WithDirection(new double[] { index, index, index }).Build(),
                new FingerBuilder().OfType(FingerType.Index).WithDirection(new double[] { index, index, index }).Build(),
                new FingerBuilder().OfType(FingerType.Middle).WithDirection(new double[] { index, index, index }).Build(),
                new FingerBuilder().OfType(FingerType.Ring).WithDirection(new double[] { index, index, index }).Build(),
                new FingerBuilder().OfType(FingerType.Pinky).WithDirection(new double[] { index, index, index }).Build()
            };

            return new HandBuilder()
                    .WithFingers(fingers)
                    .WithPalmNormal(new double[] { index, index, index })
                    .WithHandDirection(new double[] { index, index, index })
                    .Build();
        }
    }
}
