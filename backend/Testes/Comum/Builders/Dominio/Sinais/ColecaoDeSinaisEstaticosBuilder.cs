using System;
using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class ColecaoDeSinaisEstaticosBuilder
    {
        private int size = 2;
        private int sampleCount = 4;
        private string descriptionTemplate = "{0}";
        private string pathTemplate = "{0}";
        private Func<int, Amostra> sampleGenerator;


        public ColecaoDeSinaisEstaticosBuilder()
        {
            sampleGenerator = index => new AmostraBuilder().Construir();
        }

        public ColecaoDeSinaisEstaticosBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder WithDescriptionTemplate(string descriptionTemplate)
        {
            this.descriptionTemplate = descriptionTemplate;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder WithPathTemplate(string pathTemplate)
        {
            this.pathTemplate = pathTemplate;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder WithSampleGenerator(Func<int, Amostra> sampleGenerator)
        {
            this.sampleGenerator = sampleGenerator;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder WithSampleCount(int sampleCount)
        {
            this.sampleCount = sampleCount;
            return this;
        }

        public ICollection<Sinal> Build()
        {
            var signs = new List<Sinal>();

            for (int i = 0; i < size; i++)
            {
                var signBuilder = new SinalBuilder()
                    .ComDescricao(String.Format(descriptionTemplate, i))
                    .ComCaminhoParaArquivoDeExemplo(String.Format(pathTemplate, i));

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
