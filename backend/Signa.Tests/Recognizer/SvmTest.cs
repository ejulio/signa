﻿using FluentAssertions;
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
            var sample = new SignFrameBuilder().Build();
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

            var frame = BuildSignSampleByIndex(signResultIndex).Frames[0];

            var result = Svm.Instance.Recognize(frame);

            result.Should().Be(signResultIndex);
        }

        private void GivenATrainedAlgorithm(int signCount, int samplesPerSign)
        {
            var signs = GivenACollectionOfSigns(signCount, samplesPerSign);
            var trainningData = new SvmTrainningData(signs);
            Svm.Instance.Train(trainningData);
        }

        private ICollection<Sign> GivenACollectionOfSigns(int signCount, int samplesPerSign)
        {
            var signs = new SignCollectionBuilder()
                            .WithSize(signCount)            
                            .WithSampleCount(samplesPerSign)
                            .WithSampleGenerator(i => BuildSignSampleByIndex(i))
                            .Build();
            return signs;
        }

        private SignSample BuildSignSampleByIndex(int index)
        {
            var leftHand = GivenHandWithFingers(index);
            var rightHand = GivenHandWithFingers(index);

            var frames = new[] 
            { 
                new SignFrameBuilder()
                    .WithLeftHand(leftHand)
                    .WithRightHand(rightHand)
                    .Build() 
            };

            return new SignSampleBuilder()
                .WithFrames(frames)
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
