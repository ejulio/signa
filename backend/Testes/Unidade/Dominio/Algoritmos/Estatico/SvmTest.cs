using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Caracteristicas;
using Signa.Dominio.Sinais.Estatico;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Estatico;

namespace Testes.Unidade.Dominio.Algoritmos.Estatico
{
    [TestClass]
    public class SvmTest
    {
        [TestMethod]
        public void recognizing_without_trainning_throw_an_error()
        {
            var sample = new AmostraBuilder().Construir();
            Action recognizeCall = () => new Svm().Reconhecer(sample);

            recognizeCall.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void recognizing_an_input()
        {
            const int signCount = 3;
            const int samplesPerSign = 2;
            const int signResultIndex = 2;

            var svm = GivenATrainedAlgorithm(signCount, samplesPerSign);

            var sample = BuildSignSampleByIndex(signResultIndex);

            var result = svm.Reconhecer(sample);

            result.Should().Be(signResultIndex);
        }

        private Svm GivenATrainedAlgorithm(int signCount, int samplesPerSign)
        {
            var signs = GivenACollectionOfSigns(signCount, samplesPerSign);
            var trainningData = new SignRecognitionAlgorithmData(signs);

            var svm = new Svm();
            svm.Train(trainningData);

            return svm;
        }

        private ICollection<SinalEstatico> GivenACollectionOfSigns(int signCount, int samplesPerSign)
        {
            var signs = new StaticSignCollectionBuilder()
                            .WithSize(signCount)            
                            .WithSampleCount(samplesPerSign)
                            .WithSampleGenerator(BuildSignSampleByIndex)
                            .Build();
            return signs;
        }

        private Sample BuildSignSampleByIndex(int index)
        {
            var leftHand = GivenHandWithFingers(index);
            var rightHand = GivenHandWithFingers(index);

            return new AmostraBuilder()
                .WithLeftHand(leftHand)
                .WithRightHand(rightHand)
                .Construir();
        }

        private Mao GivenHandWithFingers(int index)
        {
            var fingers = new[] 
            {
                new DedoBuilder().DoTipo(TipoDeDedo.Dedao).ComDirecao(new double[] { index, index, index }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Indicador).ComDirecao(new double[] { index, index, index }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Meio).ComDirecao(new double[] { index, index, index }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Anelar).ComDirecao(new double[] { index, index, index }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Mindinho).ComDirecao(new double[] { index, index, index }).Construir()
            };

            return new MaoBuilder()
                    .ComDedos(fingers)
                    .ComVetorNormalDaPalma(new double[] { index, index, index })
                    .ComDirecaoDaMao(new double[] { index, index, index })
                    .Construir();
        }
    }
}
