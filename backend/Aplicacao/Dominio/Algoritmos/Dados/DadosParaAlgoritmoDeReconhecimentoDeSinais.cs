using Signa.Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Dominio.Algoritmos.Dados
{
    public abstract class DadosParaAlgoritmoDeReconhecimentoDeSinais
    {
        public int[] Saidas { get; private set; }
        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<Sinal> sinais;
        private LinkedList<int> saidas;

        protected DadosParaAlgoritmoDeReconhecimentoDeSinais(IEnumerable<Sinal> sinais)
        {
            this.sinais = sinais;
            saidas = new LinkedList<int>();
            InicializarExtracaoDeInformacoesDosSinais();
            ExtrairInformacoesDosSinais();
            FinalizarExtracaoDeInformacoesDosSinais();
        }

        protected abstract void InicializarExtracaoDeInformacoesDosSinais();

        public void ExtrairInformacoesDosSinais()
        {
            QuantidadeDeClasses = 0;
            saidas = new LinkedList<int>();

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    saidas.AddLast(sinal.Id);
                    GerarEntradaParaAAmostra(amostra);
                }
                QuantidadeDeClasses++;
            }

            Saidas = saidas.ToArray();
        }

        protected abstract void GerarEntradaParaAAmostra(IList<Frame> amostra);
        protected abstract void FinalizarExtracaoDeInformacoesDosSinais();
    }
}