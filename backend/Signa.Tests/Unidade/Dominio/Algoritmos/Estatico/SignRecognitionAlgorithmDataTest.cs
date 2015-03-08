using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Algoritmos.Estatico;
using Signa.Domain.Sinais.Estatico;
using Signa.Testes.Comum.Builders.Dominio.Sinais;
using Signa.Testes.Comum.Builders.Dominio.Sinais.Estatico;

namespace Signa.Testes.Dominio.Algoritmos.Estatico
{
    [TestClass]
    public class SignRecognitionAlgorithmDataTest
    {
        private readonly Sample defaultSignSample;

        public SignRecognitionAlgorithmDataTest()
        {
            defaultSignSample = new AmostraBuilder().Construir();
        }

        [TestMethod]
        public void creating_algorithm_trainning_data_with_a_collection_of_one_sign()
        {
            var signs = GivenACollectionOfOneSign();

            var algorithmData = new SignRecognitionAlgorithmData(signs);

            MustHaveOneTrainningData(algorithmData);
        }

        [TestMethod]
        public void creating_algorithm_trainning_data_with_a_collection_signs()
        {
            const int samplesPerSign = 5;
            const int signCount = 6;

            var signs = GivenACollectionOfSigns(samplesPerSign, signCount);

            var algorithmData = new SignRecognitionAlgorithmData(signs);

            MustHaveTheDataOfTheCollectionOfSigns(algorithmData, signs, samplesPerSign);
        }

        private ICollection<SinalEstatico> GivenACollectionOfOneSign()
        {
            Func<int, Sample> sampleGenerator = index => defaultSignSample;

            var signs = new StaticSignCollectionBuilder()
                        .WithSampleCount(1)
                        .WithSampleGenerator(sampleGenerator)
                        .WithSize(1)
                        .Build();

            return signs;
        }

        private static ICollection<SinalEstatico> GivenACollectionOfSigns(int samplesPerSign, int signCount)
        {
            var signs = new StaticSignCollectionBuilder()
                        .WithSampleCount(samplesPerSign)
                        .WithSize(signCount)
                        .Build();

            return signs;
        }

        private void MustHaveTheDataOfTheCollectionOfSigns(SignRecognitionAlgorithmData algorithmData, ICollection<SinalEstatico> signs, int samplesPerSign)
        {
            algorithmData.QuantidadeDeClasses.Should().Be(signs.Count);
            algorithmData.Entradas.Should().HaveCount(samplesPerSign * signs.Count);
            algorithmData.Entradas.Should().HaveSameCount(algorithmData.Saidas);
            algorithmData.Saidas.Should().ContainInOrder(ExpectedOutputArrayFor(signs.Count, samplesPerSign));

            int inputIndex = 0;
            var expectedInputs = ExpectedInputArrayFor(signs);
            foreach (var input in algorithmData.Entradas)
            {
                input.Should().ContainInOrder(expectedInputs[inputIndex]);
                inputIndex++;
            }
        }

        private void MustHaveOneTrainningData(SignRecognitionAlgorithmData algorithmData)
        {
            algorithmData.QuantidadeDeClasses.Should().Be(1);
            algorithmData.Entradas.Should().HaveCount(1);
            algorithmData.Entradas[0].Should().ContainInOrder(defaultSignSample.ToArray());
            algorithmData.Saidas.Should().HaveCount(1);
            algorithmData.Saidas[0].Should().Be(0);
        }

        private double[][] ExpectedInputArrayFor(ICollection<SinalEstatico> signs)
        {
            var inputs = new LinkedList<double[]>();

            foreach (var sign in signs)
            {
                foreach (var sample in sign.Amostras)
                {
                    inputs.AddFirst(sample.ToArray());
                }
            }

            return inputs.ToArray();
        }

        private IEnumerable ExpectedOutputArrayFor(int signCount, int samplesPerSign)
        {
            int[] output = new int[signCount * samplesPerSign];
            int signIndex = signCount - 1;

            int i = 0;
            while (i < output.Length)
            {
                output[i] = signIndex;

                i++;

                if (i % samplesPerSign == 0)
                {
                    signIndex--;
                }
            }

            return output;
        }
    }
}
