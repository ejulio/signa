using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class Svm : IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        private MulticlassSupportVectorMachine svm;

        public int Reconhecer(IList<Frame> frame)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            var geradorDeAmostra = new GeradorDeCaracteristicasDeSinalEstatico();

            return svm.Compute(geradorDeAmostra.ExtrairCaracteristicasDaAmostra(frame));
        }

        public void Treinar(IGeradorDeDadosDeSinaisEstaticos dados)
        {
            svm = new MulticlassSupportVectorMachine(0, dados.QuantidadeDeClasses);

            var teacher = new MulticlassSupportVectorLearning(svm, dados.Entradas, dados.Saidas);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) => 
                                    new SequentialMinimalOptimization(machine, classInputs, classOutputs);

            teacher.Run();
        }
    }
}