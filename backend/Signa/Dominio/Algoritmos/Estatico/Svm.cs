using System;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Signa.Dominio.Sinais.Estatico;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class Svm : IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        private MulticlassSupportVectorMachine svm;

        public int Reconhecer(Sample data)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            return svm.Compute(data.ToArray());
        }

        public void Train(IDadosParaAlgoritmoDeReconhecimentoDeSinal data)
        {
            svm = new MulticlassSupportVectorMachine(0, data.QuantidadeDeClasses);

            var teacher = new MulticlassSupportVectorLearning(svm, data.Entradas, data.Saidas);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => 
                                    new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }
    }
}