using System.Collections.Generic;
using Signa.Domain.Sinais.Dinamico;

namespace Signa.Testes.Comum.Builders.Dominio.Sinais.Dinamico
{
    public class SinalBuilder
    {
        private string description;
        private string path;
        private IList<AmostraDeSinal> samples;

        public SinalBuilder()
        {
            samples = new List<AmostraDeSinal>();
        }

        public SinalBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public SinalBuilder WithPath(string path)
        {
            this.path = path;
            return this;
        }

        public SinalBuilder ComAmostra(AmostraDeSinal sample)
        {
            this.samples.Add(sample);
            return this;
        }

        public SinalDinamico Construir()
        {
            return new SinalDinamico
            {
                Descricao = description,
                CaminhoParaArquivoDeExemplo = path,
                Amostras = samples
            };
        }
    }
}
