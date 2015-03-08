using System.Collections.Generic;
using Signa.Domain.Sinais.Estatico;

namespace Testes.Comum.Builders.Dominio.Sinais.Estatico
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
