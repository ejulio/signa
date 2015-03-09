using System;
using System.Collections.Generic;
using System.Linq;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class DadosParaAlgoritmoDeReconhecimentoDeSinal : IDadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        public double[][] Entradas { get; private set; }
        public int[] Saidas { get; private set; }
        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<Sinal> sinais;
        private LinkedList<int> saidas;
        private LinkedList<double[]> entradas;

        public DadosParaAlgoritmoDeReconhecimentoDeSinal(IEnumerable<Sinal> sinais)
        {
            this.sinais = sinais;
            ExtrairInformacoesDosSinais();
        }

        public void ExtrairInformacoesDosSinais()
        {
            int indiceDoSinal = 0;
            QuantidadeDeClasses = 0;
            entradas = new LinkedList<double[]>();
            saidas = new LinkedList<int>();

            foreach (var sinal in sinais)
            {
                ExtrairDadosDasAmostras(sinal, indiceDoSinal);
                QuantidadeDeClasses++;
                indiceDoSinal++;
            }

            Saidas = saidas.ToArray();
            Entradas = entradas.ToArray();
        }

        private void ExtrairDadosDasAmostras(Sinal sinalEstatico, int indiceDoSinal)
        {
            foreach (IAmostraDeSinalEstatico amostra in sinalEstatico.Amostras)
            {
                saidas.AddFirst(indiceDoSinal);
                entradas.AddFirst(amostra.ParaArray());
            }
        }
    }
}