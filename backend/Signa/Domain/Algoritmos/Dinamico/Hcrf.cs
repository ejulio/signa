using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Signa.Domain.Sinais.Dinamico;
using System;

namespace Signa.Domain.Algoritmos.Dinamico
{
    public class Hcrf
    {
        private HiddenMarkovClassifier<Independent<NormalDistribution>> classificador;

        public int Reconhecer(AmostraDeSinal amostra)
        {
            if (classificador == null)
                throw new InvalidOperationException();

            return classificador.Compute(amostra.ToArray());
        }

        public void TreinarCom(DadosParaAlgoritmoDeReconhecimentoDeSinal dados)
        {
            var initial = CriarDistribuicao(dados.Entradas);


            int numberOfStates = 5; // this value can be found by trial-and-error

            classificador = new HiddenMarkovClassifier<Independent<NormalDistribution>>
            (
               classes: dados.QuantidadeDeClasses,
               topology: new Forward(numberOfStates), // word classifiers should use a forward topology
               initial: initial
            );

            // Create a new learning algorithm to train the sequence classifier
            var teacher = new HiddenMarkovClassifierLearning<Independent<NormalDistribution>>(classificador,

                // Train each model until the log-likelihood changes less than 0.001
                modelIndex => new BaumWelchLearning<Independent<NormalDistribution>>(classificador.Models[modelIndex])
                {
                    Tolerance = 0.001,
                    Iterations = 100,

                    // This is necessary so the code doesn't blow up when it realize
                    // there is only one sample per word class. But this could also be
                    // needed in normal situations as well.
                    //
                    FittingOptions = new IndependentOptions()
                    {
                        InnerOption = new NormalOptions() { Regularization = 1e-5 }
                    }
                }
            );

            // Finally, we can run the learning algorithm!
            teacher.Run(dados.Entradas, dados.Saidas);
        }

        private static Independent<NormalDistribution> CriarDistribuicao(double[][][] entradas)
        {
            // TODO: Como a distribuição é por feature do vetor, verificar para essa implementação ficar nos dados de treinamento
            var distribuicoes = new NormalDistribution[entradas[0][0].Length];

            for (int i = 0; i < distribuicoes.Length; i++)
            {
                distribuicoes[i] = new NormalDistribution(0, 1);
            }

            return new Independent<NormalDistribution>(distribuicoes);
        }
    }
}