using Accord.Controls;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dados;
using Dominio.Sinais;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Tuple<int, int>[] path;
            double output;
            //return svm.Compute(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(frame), MulticlassComputeMethod.Elimination, out path);
            var p = geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(frame).Select(v => v.ToString());
            Console.WriteLine(string.Join("; ", p));
            return svm.Compute(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(frame), out output, out path);
        }

        public void Treinar(IGeradorDeDadosDeSinaisEstaticos dados)
        {
            var kernel = new Gaussian(1);
            svm = new MulticlassSupportVectorMachine(0, kernel, dados.QuantidadeDeClasses);
            
            var teacher = new MulticlassSupportVectorLearning(svm, dados.Entradas, dados.Saidas)
            {
                Algorithm = (machine, classInputs, classOutputs, j, k) => 
                {
                    var smo = new SequentialMinimalOptimization(machine, classInputs, classOutputs);
                    smo.Complexity = 1;
                    return smo;
                }
            };
            

            var e = teacher.Run();
        }
    }
}