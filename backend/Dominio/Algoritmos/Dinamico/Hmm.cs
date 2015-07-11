using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Treinamento;
using Dominio.Sinais.Frames;
using System;
using System.Collections.Generic;

namespace Dominio.Algoritmos.Dinamico
{
    public class Hmm : IAlgoritmoClassificacaoSinaisDinamicos
    {
        private const int QuantidadeEstados = 5;
        private readonly ICaracteristicasSinalDinamico caracteristicas;
        private HiddenMarkovClassifier<MultivariateNormalDistribution> hmm;

        public Hmm(ICaracteristicasSinalDinamico caracteristicas)
        {
            this.caracteristicas = caracteristicas;
        }

        public int Classificar(IList<Frame> amostra)
        {
            if (hmm == null)
                throw new InvalidOperationException();

            var caracteristicasDoSinal = caracteristicas.DaAmostra(amostra);
            return hmm.Compute(caracteristicasDoSinal);
        }

        public void Aprender(IDadosSinaisDinamicos dados)
        {
            var quantidadeCaracteristicas = dados.CaracteristicasSinais[0][0].Length;
            hmm = new HiddenMarkovClassifier<MultivariateNormalDistribution>(
                classes: dados.QuantidadeClasses,
                topology: new Forward(QuantidadeEstados),
                initial: new MultivariateNormalDistribution(quantidadeCaracteristicas)
                );

            var teacher = new HiddenMarkovClassifierLearning<MultivariateNormalDistribution>(hmm,
                modelIndex => new BaumWelchLearning<MultivariateNormalDistribution>(hmm.Models[modelIndex])
                {
                    Tolerance = 0.001,
                    Iterations = 100,
                    FittingOptions = new NormalOptions { Regularization = 1e-5}
                });

            teacher.Run(dados.CaracteristicasSinais, dados.IdentificadoresSinais);
        }
    }
}