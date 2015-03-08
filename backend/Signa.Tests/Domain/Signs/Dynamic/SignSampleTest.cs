﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Signs.Dynamic;
using System;
using System.Linq;
using Signa.Tests.Common.Builders.Domain.Signs.Dinamico;

namespace Signa.Tests.Domain.Signs.Dynamic
{
    [TestClass]
    public class SignSampleTest
    {
        [TestMethod]
        public void building_a_sample_with_one_frame()
        {
            var frames = GivenAnArrayOfSignFramesWithCount(1);
            var signSample = new AmostraDeSinalBuilder()
                .ComFrames(frames)
                .Construir();

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithFrameData(frames, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_two_frames()
        {
            var frames = GivenAnArrayOfSignFramesWithCount(2);
            var signSample = new AmostraDeSinalBuilder()
                .ComFrames(frames)
                .Construir();

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithFrameData(frames, sampleArray);
        }

        [TestMethod]
        public void building_a_sample_with_random_number_of_frames()
        {
            var numberOfFrames = new Random().Next(3, 15);
            var frames = GivenAnArrayOfSignFramesWithCount(numberOfFrames);
            var signSample = new AmostraDeSinalBuilder()
                .ComFrames(frames)
                .Construir();

            var sampleArray = signSample.ToArray();

            MustReturnAnArrayWithFrameData(frames, sampleArray);
        }

        private FrameDeSinal[] GivenAnArrayOfSignFramesWithCount(int count)
        {
            var frames = new FrameDeSinal[count];

            for (var i = 0; i < count; i++)
            {
                frames[i] = new FrameDeSinalBuilder().Construir();
            }

            return frames;
        }

        private void MustReturnAnArrayWithFrameData(FrameDeSinal[] framesDeSinal, double[][] sampleArray)
        {
            var expectedFrameData = framesDeSinal.Select(f => f.ToArray());

            sampleArray.Should().HaveCount(expectedFrameData.Count());

            for (var i = 0; i < expectedFrameData.Count(); i++)
            {
                sampleArray[i].Should().HaveSameCount(expectedFrameData.ElementAt(i));
                sampleArray[i].Should().ContainInOrder(expectedFrameData.ElementAt(i));
            }
        }
    }
}
