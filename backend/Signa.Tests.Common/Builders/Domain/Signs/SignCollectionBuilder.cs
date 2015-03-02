using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Signs.Dynamic;
using System;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs
{
    public class SignCollectionBuilder
    {
        private int size = 2;
        private int sampleCount = 4;
        private string descriptionTemplate = "{0}";
        private string pathTemplate = "{0}";
        private Func<int, SignSample> sampleGenerator;


        public SignCollectionBuilder()
        {
            sampleGenerator = index =>
            {
                var length = index + 1;
                var frames = new SignFrame[length];

                for (var i = 0; i < length; i++)
                {
                    frames[i] = new SignFrameBuilder().Build();
                }

                return new SignSample
                {
                    Frames = frames
                };
            };
        }

        public SignCollectionBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public SignCollectionBuilder WithDescriptionTemplate(string descriptionTemplate)
        {
            this.descriptionTemplate = descriptionTemplate;
            return this;
        }

        public SignCollectionBuilder WithPathTemplate(string pathTemplate)
        {
            this.pathTemplate = pathTemplate;
            return this;
        }

        public SignCollectionBuilder WithSampleGenerator(Func<int, SignSample> sampleGenerator)
        {
            this.sampleGenerator = sampleGenerator;
            return this;
        }

        public SignCollectionBuilder WithSampleCount(int sampleCount)
        {
            this.sampleCount = sampleCount;
            return this;
        }

        public ICollection<Sign> Build()
        {
            var signs = new List<Sign>();
            SignBuilder signBuilder;

            for (int i = 0; i < size; i++)
            {
                signBuilder = new SignBuilder()
                    .WithDescription(String.Format(descriptionTemplate, i))
                    .WithPath(String.Format(pathTemplate, i));

                for (int j = 0; j < sampleCount; j++)
                {
                    signBuilder.WithSample(sampleGenerator(i));
                }

                signs.Add(signBuilder.Build());
            }

            return signs;
        }
    }
}
