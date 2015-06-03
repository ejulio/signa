using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
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
    public class Hcrf : IAlgoritmoClassificacaoSinaisDinamicos
    {
        private const int QuantidadeEstados = 5;
        private readonly ICaracteristicasSinalDinamico caracteristicas;
        private HiddenConditionalRandomField<double[]> hcrf;

        public Hcrf(ICaracteristicasSinalDinamico caracteristicas)
        {
            this.caracteristicas = caracteristicas;
        }

        public int Classificar(IList<Frame> amostra)
        {
            if (hcrf == null)
                throw new InvalidOperationException();

            var caracteristicasDoSinal = caracteristicas.DaAmostra(amostra);
            return hcrf.Compute(caracteristicasDoSinal); ;
        }

        public void Aprender(IDadosSinaisDinamicos dados)
        {
            var classificadorHmm = AprenderClassificarHmm(dados);

            var funcaoPotencial = new MarkovMultivariateFunction(classificadorHmm);
            hcrf = new HiddenConditionalRandomField<double[]>(funcaoPotencial);
        }

        private HiddenMarkovClassifier<Independent<NormalDistribution>> AprenderClassificarHmm(IDadosSinaisDinamicos dados)
        {
            var distribuicoes = DistribuicoesNormais(dados.CaracteristicasSinais);

            var classificadorHmm = new HiddenMarkovClassifier<Independent<NormalDistribution>>(
                classes: dados.QuantidadeClasses,
                topology: new Forward(QuantidadeEstados),
                initial: distribuicoes
                );

            var teacher = new HiddenMarkovClassifierLearning<Independent<NormalDistribution>>(classificadorHmm,
                modelIndex => new BaumWelchLearning<Independent<NormalDistribution>>(classificadorHmm.Models[modelIndex])
                {
                    Tolerance = 0.001,
                    Iterations = 100,
                    FittingOptions = new IndependentOptions()
                    {
                        InnerOption = new NormalOptions() {Regularization = 1e-5}
                    }
                }
                );

            teacher.Run(dados.CaracteristicasSinais, dados.IdentificadoresSinais);
            return classificadorHmm;
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