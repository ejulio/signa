using System;
using System.Collections.Generic;
using Signa.Dominio.Sinais.Estatico;
using Testes.Comum.Builders.Dominio.Sinais.Estatico;

namespace Testes.Comum.Builders.Dominio.Sinais
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
            sampleGenerator = index => new AmostraBuilder().Construir();
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

        public ICollection<SinalEstatico> Build()
        {
            var signs = new List<SinalEstatico>();

            for (int i = 0; i < size; i++)
            {
                var signBuilder = new SinalBuilder()
                    .ComDescricao(String.Format(descriptionTemplate, i))
                    .WithPath(String.Format(pathTemplate, i));

                for (int j = 0; j < sampleCount; j++)
                {
                    signBuilder.ComAmostra(sampleGenerator(i));
                }

                signs.Add(signBuilder.Construir());
            }

            return signs;
        }
    }
}
