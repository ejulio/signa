using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Treinamento;
using Dominio.Sinais.Frames;
using System;
using System.Collections.Generic;

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
            var caracteristicasDoSinal = caracteristicas.DaAmostra(frame);
            return svm.Compute(caracteristicasDoSinal, MulticlassComputeMethod.Elimination);
        }

        public void Aprender(IDadosSinaisEstaticos dados)
        {
            var kernel = new Gaussian(sigma: 1);
            svm = new MulticlassSupportVectorMachine(QuantidadeIndeterminadaDeCaracteristicas, kernel, dados.QuantidadeClasses);
            
            var teacher = new MulticlassSupportVectorLearning(svm, dados.CaracteristicasSinais, dados.IdentificadoresSinais)
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