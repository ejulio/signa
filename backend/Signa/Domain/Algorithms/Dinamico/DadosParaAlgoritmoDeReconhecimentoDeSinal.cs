using System;
using System.Collections.Generic;
using System.Linq;
using Signa.Domain.Algorithms.Static;
using Signa.Domain.Signs.Dynamic;

namespace Signa.Domain.Algorithms.Dinamico
{
    public class DadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        public double[][][] Entradas { get; private set; }

        public int[] Saidas { get; private set; }

        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<AmostraDeSinal> amostras;
        private LinkedList<double[][]> entradas;
        private LinkedList<int> saidas; 

        public DadosParaAlgoritmoDeReconhecimentoDeSinal(IEnumerable<AmostraDeSinal> amostras)
        {
            this.amostras = amostras;
            entradas = new LinkedList<double[][]>();
            saidas = new LinkedList<int>();
            ExtrairDadosDos();
        }

        private void ExtrairDadosDos()
        {
            QuantidadeDeClasses = 0;
            int indiceDaAmostra = 0;

            foreach (var amostra in amostras)
            {
                entradas.AddFirst(amostra.ToArray());
                saidas.AddFirst(indiceDaAmostra);
                indiceDaAmostra++;
                QuantidadeDeClasses++;
            }

            Entradas = entradas.ToArray();
            Saidas = saidas.ToArray();
        }
    }
}