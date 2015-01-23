using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Recognizer;
using Signa.CommandLineInterface;
using FluentAssertions;
using Signa.Data;
using Signa.Model;

namespace Signa.Tests.CommandLineInterface
{
    [TestClass]
    public class LoadCommandTest
    {
        ICommand command;
        Mock<ITrainableAlgorithm> algorithmMock;
        Mock<IDataLoader> dataLoaderMock;

        [TestInitialize]
        public void CreateAlgorithm()
        {
            algorithmMock = new Mock<ITrainableAlgorithm>();
            dataLoaderMock = new Mock<IDataLoader>();

            command = new LoadCommand(dataLoaderMock.Object, algorithmMock.Object);
        }

        [TestMethod]
        public void MatchName_Return_True_For_treinar()
        {
            command.MatchName("carregar-exemplos").Should().BeTrue();
        }

        [TestMethod]
        public void Execute_Calls_Train_With_Data()
        {
            var signSamples = new SignSamples();
            signSamples.Generate();
            dataLoaderMock.SetupGet(d => d.Data).Returns(signSamples.Data);

            command.Execute();
            
            dataLoaderMock.Verify(d => d.Load());
            algorithmMock.Verify(a => a.Train(It.IsNotNull<ITrainableAlgorithmData>()));
        }
    }
}
