using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Signs.Dynamic;
using System;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs
{
    public class ColecaoDeSinaisDinamicosBuilder
    {
        private int size = 2;
        private int sampleCount = 4;
        private string descriptionTemplate = "{0}";
        private string pathTemplate = "{0}";
        private Func<int, AmostraDeSinal> sampleGenerator;


        public ColecaoDeSinaisDinamicosBuilder()
        {
            sampleGenerator = index =>
            {
                var length = index + 2;
                var frames = new SignFrame[length];

                for (var i = 0; i < length; i++)
                {
                    frames[i] = new FrameDeSinalBuilder().Construir();
                }

                return new AmostraDeSinal
                {
                    Frames = frames
                };
            };
        }

        public ColecaoDeSinaisDinamicosBuilder ComTamanho(int size)
        {
            this.size = size;
            return this;
        }

        public ColecaoDeSinaisDinamicosBuilder ComTemplateDeDescricao(string descriptionTemplate)
        {
            this.descriptionTemplate = descriptionTemplate;
            return this;
        }

        public ColecaoDeSinaisDinamicosBuilder ComTemplateDeCaminho(string pathTemplate)
        {
            this.pathTemplate = pathTemplate;
            return this;
        }

        public ColecaoDeSinaisDinamicosBuilder ComGeradorDeAmostras(Func<int, AmostraDeSinal> sampleGenerator)
        {
            this.sampleGenerator = sampleGenerator;
            return this;
        }

        public ColecaoDeSinaisDinamicosBuilder ComQuantidadeDeAmostras(int sampleCount)
        {
            this.sampleCount = sampleCount;
            return this;
        }

        public ICollection<SinalDinamico> Construir()
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
                    sinalBuilder.ComAmostra(sampleGenerator(i));
                }

                signs.Add(sinalBuilder.Construir());
            }

            return signs;
        }
    }
}
