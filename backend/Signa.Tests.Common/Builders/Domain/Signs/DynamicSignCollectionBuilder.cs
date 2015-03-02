using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Signs.Static;
using System;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs
{
    public class StaticSignCollectionBuilder
    {
        private int size = 2;
        private int sampleCount = 4;
        private string descriptionTemplate = "{0}";
        private string pathTemplate = "{0}";
        private Func<int, Sample> sampleGenerator;


        public StaticSignCollectionBuilder()
        {
            sampleGenerator = index => new SampleBuilder().Build();
        }

        public StaticSignCollectionBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public StaticSignCollectionBuilder WithDescriptionTemplate(string descriptionTemplate)
        {
            this.descriptionTemplate = descriptionTemplate;
            return this;
        }

        public StaticSignCollectionBuilder WithPathTemplate(string pathTemplate)
        {
            this.pathTemplate = pathTemplate;
            return this;
        }

        public StaticSignCollectionBuilder WithSampleGenerator(Func<int, Sample> sampleGenerator)
        {
            this.sampleGenerator = sampleGenerator;
            return this;
        }

        public StaticSignCollectionBuilder WithSampleCount(int sampleCount)
        {
            this.sampleCount = sampleCount;
            return this;
        }

        public ICollection<Sign> Build()
        {
            var signs = new List<Sign>();

            for (int i = 0; i < size; i++)
            {
                var signBuilder = new SignBuilder()
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
