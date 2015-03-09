using System;
using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class ColecaoDeSinaisEstaticosBuilder
    {
        private int quantidadeDeSinais = 2;
        private int quantidadeDeAmostrasPorSinal = 4;
        private string templateDaDescricao = "{0}";
        private string templateDoCaminhoDoArquivoDeExemplo = "{0}";
        private Func<int, Amostra> geradorDeAmostras;


        public ColecaoDeSinaisEstaticosBuilder()
        {
            var frames = new[] {new FrameBuilder().Construir()};
            geradorDeAmostras = index => new AmostraBuilder().ComFrames(frames).Construir();
        }

        public ColecaoDeSinaisEstaticosBuilder ComQuantidadeDeSinais(int quantidadeDeSinais)
        {
            this.quantidadeDeSinais = quantidadeDeSinais;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder ComTemplateDeDescricao(string templateDeDescricao)
        {
            this.templateDaDescricao = templateDeDescricao;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder ComTemplateDoCaminhoDoArquivoDeExemplo(string templateDoCaminhoDoArquivoDeExemplo)
        {
            this.templateDoCaminhoDoArquivoDeExemplo = templateDoCaminhoDoArquivoDeExemplo;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder ComGeradorDeAmostras(Func<int, Amostra> geradorDeAmostras)
        {
            this.geradorDeAmostras = geradorDeAmostras;
            return this;
        }

        public ColecaoDeSinaisEstaticosBuilder ComQuantidadeDeAmostrasPorSinal(int quantidadeDeAmostrasPorSinal)
        {
            this.quantidadeDeAmostrasPorSinal = quantidadeDeAmostrasPorSinal;
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
