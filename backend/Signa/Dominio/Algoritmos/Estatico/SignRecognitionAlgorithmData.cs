using System;
using System.Collections.Generic;
using System.Linq;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class SignRecognitionAlgorithmData : IDadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        public double[][] Entradas { get; private set; }
        public int[] Saidas { get; private set; }
        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<Sinal> signs;
        private LinkedList<int> outputs;
        private LinkedList<double[]> inputs;

        public SignRecognitionAlgorithmData(IEnumerable<Sinal> signs)
        {
            this.signs = signs;
            Process();
        }

        public void Process()
        {
            throw new NotImplementedException("Recuperar os dados de um sinal");
            int signIndex = 0;
            QuantidadeDeClasses = 0;
            inputs = new LinkedList<double[]>();
            outputs = new LinkedList<int>();

            foreach (var sign in signs)
            {
                ExtracSignSampleData(sign, signIndex);
                QuantidadeDeClasses++;
                signIndex++;
            }

            Saidas = outputs.ToArray();
            Entradas = inputs.ToArray();
        }

        private void ExtracSignSampleData(Sinal sinalEstatico, int signIndex)
        {
            foreach (var sample in sinalEstatico.Amostras)
            {
                outputs.AddFirst(signIndex);
                //inputs.AddFirst(sample.ToArray());
            }
        }
    }
}