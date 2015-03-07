using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Signs.Dynamic;
using System;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs
{
    public class DynamicSignCollectionBuilder
    {
        private int size = 2;
        private int sampleCount = 4;
        private string descriptionTemplate = "{0}";
        private string pathTemplate = "{0}";
        private Func<int, AmostraDeSinal> sampleGenerator;


        public DynamicSignCollectionBuilder()
        {
            sampleGenerator = index =>
            {
                var length = index + 1;
                var frames = new SignFrame[length];

                for (var i = 0; i < length; i++)
                {
                    frames[i] = new FrameDeSinalBuilder().Build();
                }

                return new AmostraDeSinal
                {
                    Frames = frames
                };
            };
        }

        public DynamicSignCollectionBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public DynamicSignCollectionBuilder WithDescriptionTemplate(string descriptionTemplate)
        {
            this.descriptionTemplate = descriptionTemplate;
            return this;
        }

        public DynamicSignCollectionBuilder WithPathTemplate(string pathTemplate)
        {
            this.pathTemplate = pathTemplate;
            return this;
        }

        public DynamicSignCollectionBuilder WithSampleGenerator(Func<int, AmostraDeSinal> sampleGenerator)
        {
            this.sampleGenerator = sampleGenerator;
            return this;
        }

        public DynamicSignCollectionBuilder WithSampleCount(int sampleCount)
        {
            this.sampleCount = sampleCount;
            return this;
        }

        public ICollection<SinalDinamico> Build()
        {
            var signs = new List<SinalDinamico>();
            SinalBuilder sinalBuilder;

            for (int i = 0; i < size; i++)
            {
                sinalBuilder = new SinalBuilder()
                    .WithDescription(String.Format(descriptionTemplate, i))
                    .WithPath(String.Format(pathTemplate, i));

                for (int j = 0; j < sampleCount; j++)
                {
                    sinalBuilder.WithSample(sampleGenerator(i));
                }

                signs.Add(sinalBuilder.Build());
            }

            return signs;
        }
    }
}
