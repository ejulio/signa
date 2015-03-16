using System;
using System.Collections.Generic;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dados;
using Dominio.Sinais;

namespace Dominio.Algoritmos.Dinamico
{
    public class Hcrf : IAlgoritmoDeReconhecimentoDeSinaisDinamicos
    {
        private readonly IGeradorDeCaracteristicasDeSinalDinamico geradorDeCaracteristicas;
        private HiddenConditionalRandomField<double[]> classificador;

        public Hcrf(IGeradorDeCaracteristicasDeSinalDinamico geradorDeCaracteristicas)
        {
            this.geradorDeCaracteristicas = geradorDeCaracteristicas;
        }

        public int Reconhecer(IList<Frame> amostra)
        {
            if (classificador == null)
                throw new InvalidOperationException();
            double p;
            int r = classificador.Compute(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(amostra), out p);
            Console.WriteLine("{0} - {1}", r, p);
            return r;
        }

        public void Treinar(IGeradorDeDadosDeSinaisDinamicos geradorDeDados)
        {
            var initial = CriarDistribuicao(geradorDeDados.Entradas);


            int numberOfStates = 5; // this value can be found by trial-and-error

            var classifier = new HiddenMarkovClassifier<Independent<NormalDistribution>>
            (
               classes: geradorDeDados.QuantidadeDeClasses,
               topology: new Forward(numberOfStates), // word classifiers should use a forward topology
               initial: initial
            );

            // Create a new learning algorithm to train the sequence classifier
            var teacher = new HiddenMarkovClassifierLearning<Independent<NormalDistribution>>(classifier,

                // Train each model until the log-likelihood changes less than 0.001
                modelIndex => new BaumWelchLearning<Independent<NormalDistribution>>(classifier.Models[modelIndex])
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
            teacher.Run(geradorDeDados.Entradas, geradorDeDados.Saidas);

            var function = new MarkovMultivariateFunction(classifier);
            classificador = new HiddenConditionalRandomField<double[]>(function);
        }

        private Independent<NormalDistribution> CriarDistribuicao(double[][][] entradas)
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