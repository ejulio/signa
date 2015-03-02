using Signa.Domain.Signs.Dynamic;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs.Dynamic
{
    public class SignBuilder
    {
        private string description;
        private string path;
        private IList<SignSample> samples;

        public SignBuilder()
        {
            samples = new List<SignSample>();
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

        public SignBuilder WithSample(SignSample sample)
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
