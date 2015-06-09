using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.Univariate;
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
        private HiddenMarkovClassifier<Independent<NormalDistribution>> hmm;

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
            var distribuicoes = DistribuicoesNormais(dados.CaracteristicasSinais);

            hmm = new HiddenMarkovClassifier<Independent<NormalDistribution>>(
                classes: dados.QuantidadeClasses,
                topology: new Forward(QuantidadeEstados),
                initial: distribuicoes
                );

            var teacher = new HiddenMarkovClassifierLearning<Independent<NormalDistribution>>(hmm,
                modelIndex => new BaumWelchLearning<Independent<NormalDistribution>>(hmm.Models[modelIndex])
                {
                    Tolerance = 0.001,
                    Iterations = 100,
                    FittingOptions = new IndependentOptions()
                    {
                        InnerOption = new NormalOptions() { Regularization = 1e-5 }
                    }
                });

            teacher.Run(dados.CaracteristicasSinais, dados.IdentificadoresSinais);
        }

        private Independent<NormalDistribution> DistribuicoesNormais(double[][][] entradas)
        {
            var distribuicoes = new NormalDistribution[entradas[0][0].Length];

            for (int i = 0; i < distribuicoes.Length; i++)
            {
                distribuicoes[i] = new NormalDistribution(0, 1);
            }

            return new Independent<NormalDistribution>(distribuicoes);
        }
    }
}