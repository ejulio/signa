using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Recognizer;
using Signa.CommandLineInterface;
using FluentAssertions;

namespace Signa.Tests.CommandLineInterface
{
    [TestClass]
    public class TrainAlgorithmCommandTest
    {
        ICommand command;
        Mock<ITrainableAlgorithm> algorithmMock;
        Mock<ITrainableAlgorithmData> dataMock;

        [TestInitialize]
        public void CreateAlgorithm()
        {
            algorithmMock = new Mock<ITrainableAlgorithm>();
            dataMock = new Mock<ITrainableAlgorithmData>();

            command = new TrainAlgorithmCommand(algorithmMock.Object, dataMock.Object);
        }

        [TestMethod]
        public void MatchName_Return_True_For_treinar()
        {
            command.MatchName("treinar").Should().BeTrue();
        }

        [TestMethod]
        public void Execute_Calls_Train_With_Data()
        {
            command.Execute();
            algorithmMock.Verify(a => a.Train(dataMock.Object));
        }
    }
}
