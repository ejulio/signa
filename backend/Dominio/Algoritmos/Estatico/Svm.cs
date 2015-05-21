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
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Estatico
{
    public class Svm : IAlgoritmoClassificacaoSinaisEstaticos
    {
        private const int QuantidadeIndeterminadaDeCaracteristicas = 0;
        private readonly ICaracteristicasSinalEstatico caracteristicas;
        private MulticlassSupportVectorMachine svm;

        public Svm(ICaracteristicasSinalEstatico caracteristicas)
        {
            this.caracteristicas = caracteristicas;
        }

        public int Classificar(IList<Frame> frame)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            return svm.Compute(caracteristicas.DaAmostra(frame), MulticlassComputeMethod.Elimination);
        }

        public void Treinar(IDadosSinaisEstaticos dados)
        {
            var kernel = new Gaussian(sigma: 1);
            svm = new MulticlassSupportVectorMachine(QuantidadeIndeterminadaDeCaracteristicas, kernel, dados.QuantidadeDeClasses);
            
            var teacher = new MulticlassSupportVectorLearning(svm, dados.Entradas, dados.Saidas)
            {
                Algorithm = (machine, classInputs, classOutputs, j, k) => 
                    new SequentialMinimalOptimization(machine, classInputs, classOutputs)
                    {
                        Complexity = 1
                    }
            };
            
            teacher.Run();
        }
    }
}