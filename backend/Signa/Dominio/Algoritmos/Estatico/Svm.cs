using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Signa.Dominio.Sinais;
using System;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class Svm : IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        private MulticlassSupportVectorMachine svm;

        public int Reconhecer(Frame frame)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            var geradorDeAmostra = new GeradorDeCaracteristicasDeSinalEstatico();

            return svm.Compute(geradorDeAmostra.ExtrairCaracteristicasDoFrame(frame));
        }

        public void Treinar(IDadosParaAlgoritmoDeReconhecimentoDeSinaisEstaticos dados)
        {
            svm = new MulticlassSupportVectorMachine(0, dados.QuantidadeDeClasses);

            var teacher = new MulticlassSupportVectorLearning(svm, dados.Entradas, dados.Saidas);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => 
                                    new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }
    }
}