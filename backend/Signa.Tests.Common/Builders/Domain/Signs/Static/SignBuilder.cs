using System.Collections.Generic;
using Signa.Domain.Signs.Dynamic;
using Signa.Domain.Signs.Static;
using Sign = Signa.Domain.Signs.Static.Sign;

namespace Signa.Tests.Common.Builders.Domain.Signs.Static
{
    public class SignBuilder
    {
        private string description;
        private string path;
        private IList<Sample> samples;

        public SignBuilder()
        {
            samples = new List<Sample>();
        }

        public SignBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public SignBuilder WithPath(string path)
        {
            this.path = path;
            return this;
        }

        public SignBuilder WithSample(Sample sample)
        {
            this.samples.Add(sample);
            return this;
        }

        public Sign Build()
        {
            return new Sign
            {
                Description = description,
                ExampleFilePath = path,
                Samples = samples
            };
        }
    }
}
