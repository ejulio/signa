using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class ColecaoDeSinaisDinamicosBuilder
    {
        private int size = 2;
        private int sampleCount = 4;
        private string descriptionTemplate = "{0}";
        private string pathTemplate = "{0}";
        private Func<int, Amostra> sampleGenerator;


        public ColecaoDeSinaisDinamicosBuilder()
        {
            sampleGenerator = index =>
            {
                var length = index + 2;
                var frames = new Frame[length];

                for (var i = 0; i < length; i++)
                {
                    frames[i] = new FrameBuilder().Construir();
                }

                return new Amostra
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

        public ColecaoDeSinaisDinamicosBuilder ComGeradorDeAmostras(Func<int, Amostra> sampleGenerator)
        {
            this.sampleGenerator = sampleGenerator;
            return this;
        }

        public ColecaoDeSinaisDinamicosBuilder ComQuantidadeDeAmostras(int sampleCount)
        {
            this.sampleCount = sampleCount;
            return this;
        }

        public ICollection<Sinal> Construir()
        {
            var signs = new List<Sinal>();
            SinalBuilder sinalBuilder;

            for (int i = 0; i < size; i++)
            {
                sinalBuilder = new SinalBuilder()
                    .ComDescricao(String.Format(descriptionTemplate, i))
                    .ComCaminhoParaArquivoDeExemplo(String.Format(pathTemplate, i));

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
