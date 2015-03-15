using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Signa.Dominio.Algoritmos.Caracteristicas;
using Signa.Dominio.Algoritmos.Dados;
using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Estatico
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
            int r = svm.Compute(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(frame), out p);
            Console.WriteLine("{0} - {1}", r, p);
            return r;
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