using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
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
            double p;
            int r = svm.Compute(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(frame), MulticlassComputeMethod.Voting, out p);
            Console.WriteLine("{0} - {1}", r, p);
            if (p > 0.6)
                return r;

            return -1;
        }

        public void Treinar(IGeradorDeDadosDeSinaisEstaticos dados)
        {
            svm = new MulticlassSupportVectorMachine(0, dados.QuantidadeDeClasses);

            var teacher = new MulticlassSupportVectorLearning(svm, dados.Entradas, dados.Saidas);
            teacher.Algorithm = (machine, classInputs, classOutputs, j, k) =>
            {
                var smo = new SequentialMinimalOptimization(machine, classInputs, classOutputs);
                smo.Run();
                return new ProbabilisticOutputCalibration(machine, classInputs, classOutputs);
            };
                                    

            teacher.Run();
        }
    }
}