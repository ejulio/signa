using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
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
        private MultilabelSupportVectorMachine svm;

        public Svm(ICaracteristicasSinalEstatico caracteristicas)
        {
            this.caracteristicas = caracteristicas;
        }

        public Tuple<int, int>[] path;

        public int Classificar(IList<Frame> frame)
        {
            if (svm == null)
            {
                throw new InvalidOperationException("É necessário treinar o algoritmo antes de reconhecer");
            }
            var caracteristicasDoSinal = caracteristicas.DaAmostra(frame);
            var resultados = svm.Compute(caracteristicasDoSinal);
            int classe;
            resultados.Max(out classe);
            return classe;
        }

        public void Aprender(IDadosSinaisEstaticos dados)
        {
            var kernel = new Linear(1);
            svm = new MultilabelSupportVectorMachine(QuantidadeIndeterminadaDeCaracteristicas, kernel, dados.QuantidadeClasses);
            
            var teacher = new MultilabelSupportVectorLearning(svm, dados.CaracteristicasSinais, dados.IdentificadoresSinais)
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