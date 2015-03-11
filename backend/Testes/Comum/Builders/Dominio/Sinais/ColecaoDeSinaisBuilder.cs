using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class ColecaoDeSinaisBuilder
    {
        private int quantidadeDeSinais = 2;
        private int quantidadeDeAmostrasPorSinal = 4;
        private string templateDaDescricao = "{0}";
        private string templateDoCaminhoDoArquivoDeExemplo = "{0}";
        private Func<int, IList<Frame>> geradorDeAmostras;


        public ColecaoDeSinaisBuilder()
        {
            geradorDeAmostras = index => new ColecaoDeFramesBuilder().Construir();
        }

        public ColecaoDeSinaisBuilder ComQuantidadeDeSinais(int quantidadeDeSinais)
        {
            this.quantidadeDeSinais = quantidadeDeSinais;
            return this;
        }

        public ColecaoDeSinaisBuilder ComTemplateDeDescricao(string templateDeDescricao)
        {
            this.templateDaDescricao = templateDeDescricao;
            return this;
        }

        public ColecaoDeSinaisBuilder ComTemplateDoCaminhoDoArquivoDeExemplo(string templateDoCaminhoDoArquivoDeExemplo)
        {
            this.templateDoCaminhoDoArquivoDeExemplo = templateDoCaminhoDoArquivoDeExemplo;
            return this;
        }

        public ColecaoDeSinaisBuilder ComGeradorDeAmostras(Func<int, IList<Frame>> geradorDeAmostras)
        {
            this.geradorDeAmostras = geradorDeAmostras;
            return this;
        }

        public ColecaoDeSinaisBuilder ComQuantidadeDeAmostrasPorSinal(int quantidadeDeAmostrasPorSinal)
        {
            this.quantidadeDeAmostrasPorSinal = quantidadeDeAmostrasPorSinal;
            return this;
        }

        public ColecaoDeSinaisBuilder ComGeradorDeAmostrasEstaticas()
        {
            geradorDeAmostras = i => new[] {new FrameBuilder().Construir()};
            return this;
        }

        public ColecaoDeSinaisBuilder ComGeradorDeAmostrasDinamicas()
        {
            geradorDeAmostras = i => new[]
            {
                new FrameBuilder().Construir(), 
                new FrameBuilder().Construir(), 
                new FrameBuilder().Construir()
            };
            return this;
        }

        public ICollection<Sinal> Construir()
        {
            var sinais = new List<Sinal>();

            for (int i = 0; i < quantidadeDeSinais; i++)
            {
                var sinalBuilder = new SinalBuilder()
                    .ComDescricao(String.Format(templateDaDescricao, i))
                    .ComCaminhoParaArquivoDeExemplo(String.Format(templateDoCaminhoDoArquivoDeExemplo, i));

                for (int j = 0; j < quantidadeDeAmostrasPorSinal; j++)
                {
                    sinalBuilder.ComAmostra(geradorDeAmostras(i));
                }

                sinais.Add(sinalBuilder.Construir());
            }

            return sinais;
        }
    }
}
