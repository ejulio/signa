using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class SinalBuilder
    {
        private string descricao;
        private string caminhoParaArquivoDeExemplo;
        private IList<IList<Frame>> amostras;

        public SinalBuilder()
        {
            amostras = new List<IList<Frame>>();
        }

        public SinalBuilder ComDescricao(string descricao)
        {
            this.descricao = descricao;
            return this;
        }

        public SinalBuilder ComCaminhoParaArquivoDeExemplo(string caminhoParaArquivoDeExemplo)
        {
            this.caminhoParaArquivoDeExemplo = caminhoParaArquivoDeExemplo;
            return this;
        }

        public SinalBuilder ComAmostra(IList<Frame> amostra)
        {
            this.amostras.Add(amostra);
            return this;
        }

        public Sinal Construir()
        {
            return new Sinal
            {
                Descricao = descricao,
                CaminhoParaArquivoDeExemplo = caminhoParaArquivoDeExemplo,
                Amostras = amostras
            };
        }
    }
}
