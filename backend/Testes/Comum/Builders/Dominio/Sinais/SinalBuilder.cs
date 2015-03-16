using System.Collections.Generic;
using Aplicacao.Dominio.Sinais;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class SinalBuilder
    {
        private string descricao;
        private string caminhoParaArquivoDeExemplo;
        private IList<IList<Frame>> amostras;
        private int id;

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

        public SinalBuilder ComId(int id)
        {
            this.id = id;
            return this;
        }

        public Sinal Construir()
        {
            return new Sinal
            {
                Id = id,
                Descricao = descricao,
                CaminhoParaArquivoDeExemplo = caminhoParaArquivoDeExemplo,
                Amostras = amostras
            };
        }
    }
}
