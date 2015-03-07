﻿using Signa.Domain.Signs.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Domain.Algorithms.Dinamico
{
    public class DadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        public double[][][] Entradas { get; private set; }

        public int[] Saidas { get; private set; }

        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<SinalDinamico> sinais;
        private LinkedList<double[][]> entradas;
        private LinkedList<int> saidas; 

        public DadosParaAlgoritmoDeReconhecimentoDeSinal(IEnumerable<SinalDinamico> sinais)
        {
            this.sinais = sinais;
            entradas = new LinkedList<double[][]>();
            saidas = new LinkedList<int>();
            ExtrairDadosDos();
        }

        private void ExtrairDadosDos()
        {
            QuantidadeDeClasses = 0;
            int identificadorDoSinal = 0;

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    entradas.AddLast(amostra.ToArray());
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