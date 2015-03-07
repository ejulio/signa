using Signa.Domain.Signs.Static;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Domain.Algorithms.Static
{
    public class SignRecognitionAlgorithmData : IDadosParaAlgoritmoDeReconhecimentoDeSinal
    {
        public double[][] Entradas { get; private set; }
        public int[] Saidas { get; private set; }
        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<SinalEstatico> signs;
        private LinkedList<int> outputs;
        private LinkedList<double[]> inputs;

        public SignRecognitionAlgorithmData(IEnumerable<SinalEstatico> signs)
        {
            this.signs = signs;
            Process();
        }

        public void Process()
        {
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

        private void ExtracSignSampleData(SinalEstatico sinalEstatico, int signIndex)
        {
            foreach (var sample in sinalEstatico.Amostras)
            {
                outputs.AddFirst(signIndex);
                inputs.AddFirst(sample.ToArray());
            }
        }
    }
}