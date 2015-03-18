using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dados;
using Dominio.Sinais;
using System;
using System.Collections.Generic;

namespace Dominio.Algoritmos.Estatico
{
    public class Svm : IAlgoritmoDeReconhecimentoDeSinaisEstaticos
    {
        private readonly IGeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicas;
        private MulticlassSupportVectorMachine svm;

        public Svm(IGeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicas)
        {
            this.geradorDeCaracteristicas = geradorDeCaracteristicas;
        }

        public int Reconhecer(IList<Frame> frame)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            return svm.Compute(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(frame), MulticlassComputeMethod.Elimination);
        }

        public void Treinar(IGeradorDeDadosDeSinaisEstaticos dados)
        {
            var kernel = new Gaussian();
            
            svm = new MulticlassSupportVectorMachine(0, kernel, dados.QuantidadeDeClasses);

            var teacher = new MulticlassSupportVectorLearning(svm, dados.Entradas, dados.Saidas)
            {
                Algorithm =
                    (machine, classInputs, classOutputs, j, k) =>
                        new SequentialMinimalOptimization(machine, classInputs, classOutputs)
            };


            var e = teacher.Run();
        }
    }
}