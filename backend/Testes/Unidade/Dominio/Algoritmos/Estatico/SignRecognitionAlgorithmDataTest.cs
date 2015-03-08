using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Estatico
{
    [TestClass]
    public class SignRecognitionAlgorithmDataTest
    {
        private readonly Amostra defaultSignSample;

        public SignRecognitionAlgorithmDataTest()
        {
            defaultSignSample = new AmostraBuilder().Construir();
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_um_sinal()
        {
            var sinais = DadaUmaColecaoDeUmSinal();

            var dadosDoAlgoritmo = new DadosParaAlgoritmoDeReconhecimentoDeSinal(sinais);

            DeveTerUmDadoDeTreinamento(dadosDoAlgoritmo);
        }

        [TestMethod]
        public void criando_dados_com_uma_colecao_de_sinais()
        {
            const int quantidadeDeAmostrasPorSinal = 5;
            const int quantidadeDeSinais = 6;

            var sinais = DadaUmaColecaoDeSinais(quantidadeDeAmostrasPorSinal, quantidadeDeSinais);

            var dadosDoAlgoritmo = new DadosParaAlgoritmoDeReconhecimentoDeSinal(sinais);

            DeveTerOsDadosDaColecaoDeSinais(dadosDoAlgoritmo, sinais, quantidadeDeAmostrasPorSinal);
        }

        private ICollection<Sinal> DadaUmaColecaoDeUmSinal()
        {
            Func<int, Amostra> sampleGenerator = index => defaultSignSample;

            var signs = new ColecaoDeSinaisEstaticosBuilder()
                        .WithSampleCount(1)
                        .WithSampleGenerator(sampleGenerator)
                        .WithSize(1)
                        .Build();

            return signs;
        }

        private static ICollection<Sinal> DadaUmaColecaoDeSinais(int samplesPerSign, int signCount)
        {
            var signs = new ColecaoDeSinaisEstaticosBuilder()
                        .WithSampleCount(samplesPerSign)
                        .WithSize(signCount)
                        .Build();

            return signs;
        }

        private void DeveTerOsDadosDaColecaoDeSinais(DadosParaAlgoritmoDeReconhecimentoDeSinal algorithmData, ICollection<Sinal> signs, int samplesPerSign)
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

        private void DeveTerUmDadoDeTreinamento(DadosParaAlgoritmoDeReconhecimentoDeSinal algorithmData)
        {
            algorithmData.QuantidadeDeClasses.Should().Be(1);
            algorithmData.Entradas.Should().HaveCount(1);
            algorithmData.Entradas[0].Should().ContainInOrder(defaultSignSample.ToArray());
            algorithmData.Saidas.Should().HaveCount(1);
            algorithmData.Saidas[0].Should().Be(0);
        }

        private double[][] ExpectedInputArrayFor(ICollection<Sinal> signs)
        {
            var inputs = new LinkedList<double[]>();

            foreach (var sign in signs)
            {
                foreach (var sample in sign.Amostras)
                {
                    throw new NotImplementedException("Utilizar a interface IAmostraDeSinalEstatico");
                    //inputs.AddFirst(sample.ToArray());
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
