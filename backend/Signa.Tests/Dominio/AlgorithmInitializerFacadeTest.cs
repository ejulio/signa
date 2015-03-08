using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Dados.Repositorio;
using Signa.Domain;
using Signa.Domain.Algoritmos;
using Signa.Domain.Algoritmos.Estatico;
using Signa.Domain.Sinais.Estatico;
using Signa.Testes.Comum.Builders.Dominio.Sinais;

namespace Signa.Testes.Dominio
{
    [TestClass]
    public class AlgorithmInitializerFacadeTest
    {
        private Mock<ISignRecognitionAlgorithmFactory> signRecognitionAlgorithmFactory;
        private Mock<IRepositorio<SinalEstatico>> repository;
        private Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos> staticSignRecognitionAlgorithm;
        private Mock<IRepositorioFactory> repositoryFactory;
        private AlgorithmInitializerFacade algorithmInitializerFacade;

        [TestInitialize]
        public void Setup()
        {
            repository = new Mock<IRepositorio<SinalEstatico>>();
            repositoryFactory = new Mock<IRepositorioFactory>();
            staticSignRecognitionAlgorithm = new Mock<IAlgoritmoDeReconhecimentoDeSinaisEstaticos>();
            signRecognitionAlgorithmFactory = new Mock<ISignRecognitionAlgorithmFactory>();

            algorithmInitializerFacade =
                new AlgorithmInitializerFacade(signRecognitionAlgorithmFactory.Object, repositoryFactory.Object);

            repositoryFactory
                .Setup(r => r.CriarECarregarRepositorioDeSinaisEstaticos())
                .Returns(repository.Object);

            signRecognitionAlgorithmFactory
                .Setup(f => f.CreateStaticSignRecognizer())
                .Returns(staticSignRecognitionAlgorithm.Object);
        }

        [TestMethod]
        public void training_static_sign_recognition_algorithm()
        {
            repository
                .Setup(r => r.GetEnumerator())
                .Returns(GivenACollectionOfSigns().GetEnumerator());

            algorithmInitializerFacade.TrainStaticSignRecognitionAlgorithm();

            staticSignRecognitionAlgorithm
                .Verify(a => 
                    a.Train(It.Is<IDadosParaAlgoritmoDeReconhecimentoDeSinal>(d => VerifyAlgorithmData(d))));
        }

        [TestMethod]
        public void training_static_sign_recognition_algorithm_without_data()
        {
            var emptySignList = new List<SinalEstatico>();
            repository
                .Setup(r => r.GetEnumerator())
                .Returns(emptySignList.GetEnumerator());

            Action action = () => algorithmInitializerFacade.TrainStaticSignRecognitionAlgorithm();

            action.ShouldNotThrow();
        }

        private ICollection<SinalEstatico> GivenACollectionOfSigns()
        {
            var signs = new StaticSignCollectionBuilder()
                        .WithSampleCount(2)
                        .WithSize(2)
                        .Build();

            return signs;
        }

        private bool VerifyAlgorithmData(IDadosParaAlgoritmoDeReconhecimentoDeSinal data)
        {
            data.QuantidadeDeClasses.Should().Be(2);
            data.Saidas.Should().HaveCount(4);
            data.Entradas.Should().HaveCount(4);
            return true;
        }
    }
}
