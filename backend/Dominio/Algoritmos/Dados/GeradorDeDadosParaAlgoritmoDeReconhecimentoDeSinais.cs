using Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Dados
{
    public abstract class GeradorDeDadosParaAlgoritmoDeReconhecimentoDeSinais
    {
        public int[] Saidas { get; private set; }
        public int QuantidadeDeClasses { get; private set; }
        public abstract int QuantidadeDeClassesPorSinal { get; }

        private readonly IEnumerable<Sinal> sinais;
        private LinkedList<int> saidas;

        protected GeradorDeDadosParaAlgoritmoDeReconhecimentoDeSinais(IEnumerable<Sinal> sinais)
        {
            this.sinais = sinais;
            saidas = new LinkedList<int>();
            ExtrairInformacoesDosSinais();
        }

        private void ExtrairInformacoesDosSinais()
        {
            var identificadorDoSinal = 0;
            QuantidadeDeClasses = 0;
            saidas = new LinkedList<int>();

            Inicializar(sinais);

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                    GerarEntradasESaidasParaAmostra(amostra, saidas, identificadorDoSinal);

                sinal.IdNoAlgoritmo = identificadorDoSinal;
                identificadorDoSinal++;
                QuantidadeDeClasses += QuantidadeDeClassesPorSinal;
            }

            Saidas = saidas.ToArray();
            Finalizar();
        }

        protected abstract void Inicializar(IEnumerable<Sinal>  sinais);
        protected abstract void GerarEntradasESaidasParaAmostra(IList<Frame> amostra, LinkedList<int> saidas, int identificadorDoSinal);
        protected abstract void Finalizar();
    }
}