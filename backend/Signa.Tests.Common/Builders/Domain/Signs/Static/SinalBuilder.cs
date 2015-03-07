using Signa.Domain.Signs.Static;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs.Static
{
    public class SinalBuilder
    {
        private string description;
        private string path;
        private IList<Sample> samples;

        public SinalBuilder()
        {
            samples = new List<Sample>();
        }

        public SinalBuilder ComDescricao(string description)
        {
            this.description = description;
            return this;
        }

        public SinalBuilder WithPath(string path)
        {
            this.path = path;
            return this;
        }

        public SinalBuilder ComAmostra(Sample sample)
        {
            this.samples.Add(sample);
            return this;
        }

        public SinalEstatico Construir()
        {
            return new SinalEstatico
            {
                Description = description,
                ExampleFilePath = path,
                Amostras = samples
            };
        }
    }
}
