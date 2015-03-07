using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Data.Repository;
using Signa.Domain;
using Signa.Domain.Algorithms;
using Signa.Domain.Algorithms.Static;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Signs;

namespace Signa.Tests.Domain
{
    [TestClass]
    public class AlgorithmInitializerFacadeTest
    {
        private Mock<ISignRecognitionAlgorithmFactory> signRecognitionAlgorithmFactory;
        private Mock<IRepository<Sign>> repository;
        private Mock<IStaticSignRecognitionAlgorithm> staticSignRecognitionAlgorithm;
        private Mock<IRepositoryFactory> repositoryFactory;
        private AlgorithmInitializerFacade algorithmInitializerFacade;

        [TestInitialize]
        public void Setup()
        {
            repository = new Mock<IRepository<Sign>>();
            repositoryFactory = new Mock<IRepositoryFactory>();
            staticSignRecognitionAlgorithm = new Mock<IStaticSignRecognitionAlgorithm>();
            signRecognitionAlgorithmFactory = new Mock<ISignRecognitionAlgorithmFactory>();

            algorithmInitializerFacade =
                new AlgorithmInitializerFacade(signRecognitionAlgorithmFactory.Object, repositoryFactory.Object);

            repositoryFactory
                .Setup(r => r.CreateAndLoadStaticSignRepository())
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
            var emptySignList = new List<Sign>();
            repository
                .Setup(r => r.GetEnumerator())
                .Returns(emptySignList.GetEnumerator());

            Action action = () => algorithmInitializerFacade.TrainStaticSignRecognitionAlgorithm();

            action.ShouldNotThrow();
        }

        private ICollection<Sign> GivenACollectionOfSigns()
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
