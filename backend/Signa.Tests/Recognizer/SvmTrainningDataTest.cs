using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Signs.Dynamic;
using Signa.Recognizer;
using Signa.Tests.Common.Builders.Domain.Signs;
using Signa.Tests.Common.Builders.Domain.Signs.Dynamic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Tests.Recognizer
{
    [TestClass]
    public class SvmTrainningDataTest
    {
        private readonly SignSample defaultSignSample;

        public SvmTrainningDataTest()
        {
            defaultSignSample = new SignSample
            {
                Frames = new[] { new SignFrameBuilder().WithDefaultLeftAndRightHand().Build() }
            };
        }

        [TestMethod]
        public void creating_algorithm_trainning_data_with_a_collection_of_one_sign()
        {
            var signs = GivenACollectionOfOneSign();

            var algorithmData = new SvmTrainningData(signs);

            MustHaveOneTrainningData(algorithmData);
        }

        [TestMethod]
        public void creating_algorithm_trainning_data_with_a_collection_signs()
        {
            const int samplesPerSign = 5;
            const int signCount = 6;

            var signs = GivenACollectionOfSigns(samplesPerSign, signCount);

            var algorithmData = new SvmTrainningData(signs);

            MustHaveTheDataOfTheCollectionOfSigns(algorithmData, signs, samplesPerSign);
        }

        private ICollection<Sign> GivenACollectionOfOneSign()
        {
            Func<int, SignSample> sampleGenerator = index => defaultSignSample;

            var signs = new SignCollectionBuilder()
                        .WithSampleCount(1)
                        .WithSampleGenerator(sampleGenerator)
                        .WithSize(1)
                        .Build();
            return signs;
        }

        private static ICollection<Sign> GivenACollectionOfSigns(int samplesPerSign, int signCount)
        {
            var signs = new SignCollectionBuilder()
                        .WithSampleCount(samplesPerSign)
                        .WithSize(signCount)
                        .Build();
            return signs;
        }

        private void MustHaveTheDataOfTheCollectionOfSigns(SvmTrainningData algorithmData, ICollection<Sign> signs, int samplesPerSign)
        {
            algorithmData.ClassCount.Should().Be(signs.Count);
            algorithmData.Inputs.Should().HaveCount(samplesPerSign * signs.Count);
            algorithmData.Inputs.Should().HaveSameCount(algorithmData.Outputs);
            algorithmData.Outputs.Should().ContainInOrder(ExpectedOutputArrayFor(signs.Count, samplesPerSign));

            int inputIndex = 0;
            var expectedInputs = ExpectedInputArrayFor(signs);
            foreach (var input in algorithmData.Inputs)
            {
                input.Should().ContainInOrder(expectedInputs[inputIndex]);
                inputIndex++;
            }
        }

        private void MustHaveOneTrainningData(SvmTrainningData algorithmData)
        {
            algorithmData.ClassCount.Should().Be(1);
            algorithmData.Inputs.Should().HaveCount(1);
            algorithmData.Inputs[0].Should().ContainInOrder(defaultSignSample.Frames[0].ToArray());
            algorithmData.Outputs.Should().HaveCount(1);
            algorithmData.Outputs[0].Should().Be(0);
        }

        private double[][] ExpectedInputArrayFor(ICollection<Sign> signs)
        {
            var inputs = new LinkedList<double[]>();

            foreach (var sign in signs)
            {
                foreach (var sample in sign.Samples)
                {
                    inputs.AddFirst(sample.Frames[0].ToArray());
                }
            }

            return inputs.ToArray();
        }

        private IEnumerable ExpectedOutputArrayFor(int signCount, int samplesPerSign)
        {
            int[] output = new int[signCount * samplesPerSign];
            int signIndex = signCount - 1;

            int i = 0;
            while (i < output.Length)
            {
                output[i] = signIndex;

                i++;

                if (i % samplesPerSign == 0)
                {
                    signIndex--;
                }
            }

            return output;
        }
    }
}
