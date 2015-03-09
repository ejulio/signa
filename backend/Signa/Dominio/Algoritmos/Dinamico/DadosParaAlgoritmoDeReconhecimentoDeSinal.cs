using System.Collections.Generic;
using System.Linq;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public class DadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        public double[][][] Entradas { get; private set; }

        public int[] Saidas { get; private set; }

        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<Sinal> sinais;
        private LinkedList<double[][]> entradas;
        private LinkedList<int> saidas; 

        public DadosParaAlgoritmoDeReconhecimentoDeSinal(IEnumerable<Sinal> sinais)
        {
            this.sinais = sinais;
            entradas = new LinkedList<double[][]>();
            saidas = new LinkedList<int>();
            ExtrairDadosDasAmostras();
        }

        private void ExtrairDadosDasAmostras()
        {
            QuantidadeDeClasses = 0;
            int identificadorDoSinal = 0;

            foreach (var sinal in sinais)
            {
                foreach (IAmostraDeSinalDinamico amostra in sinal.Amostras)
                {
                    entradas.AddLast(amostra.ParaArray());
                    saidas.AddLast(identificadorDoSinal);
                }
                identificadorDoSinal++;
                QuantidadeDeClasses++;
            }
            
            Entradas = entradas.ToArray();
            Saidas = saidas.ToArray();
        }
    }
}