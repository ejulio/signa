using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Signa.Data;
using Signa.Model;
using Signa.Tests.Common.Builders;

namespace Signa.Tests.Data
{
    [TestClass]
    public class SignControllerTest
    {
        private Mock<IRepository<Sign>> repositoryMock;
        private SignController signController;

        [TestInitialize]
        public void MyTestMethod()
        {
            repositoryMock = new Mock<IRepository<Sign>>();
            signController = new SignController(repositoryMock.Object);
        }

        [TestMethod]
        public void adding_a_sign_should_add_to_repository_if_not_exists()
        {
            const string signDescription = "new sign";
            GivenThatTheRepositoryReturnsNullForTheDescription(signDescription);

            var sign = GivenASignWithTwoSamples(signDescription);

            signController.Add(sign);

            MustAddSignToRepositoryAndSaveChanges(sign);
        }

        [TestMethod]
        public void adding_a_sign_should_merge_with_the_sign_of_the_repository_if_exists()
        {
            const string signDescription = "old sign";
            var oldSign = GivenThatTheRepositoryReturnsASignForTheDescription(signDescription);

            var newSign = GivenASignWithTwoSamples(signDescription);

            signController.Add(newSign);

            MustMergeSamplesAndSaveChanges(oldSign, newSign);
        }

        [TestMethod]
        public void getting_random_sign_should_not_return_the_last_one()
        {
            const int signIdex0 = 0;
            const int signIdex1 = 1;

            var sign0 = GivenThatTheRepositoryReturnsASignForTheIndex(signIdex0);
            var sign1 = GivenThatTheRepositoryReturnsASignForTheIndex(signIdex1);

            int signIndex;
            var randomSign = signController.GetRandomSign(signIdex1, out signIndex);

            randomSign.Should().Be(sign0);
            signIndex.Should().Be(0);
        }

        private Sign GivenThatTheRepositoryReturnsASignForTheDescription(string signDescription)
        {
            var sign = GivenASignWithTwoSamples(signDescription);

            repositoryMock
                .Setup(r => r.GetById(signDescription))
                .Returns(sign);

            return sign;
        }

        private Sign GivenThatTheRepositoryReturnsASignForTheIndex(int signIndex)
        {
            var sign = GivenASignWithTwoSamples("sign " + signIndex);

            repositoryMock
                .Setup(r => r.GetByIndex(signIndex))
                .Returns(sign);

            return sign;
        }

        private void GivenThatTheRepositoryReturnsNullForTheDescription(string signDescription)
        {
            repositoryMock.Setup(r => r.GetById(signDescription)).Returns((Sign)null);
        }

        private static Sign GivenASignWithTwoSamples(string signDescription)
        {
            var sign = new SignBuilder()
                            .WithDescription(signDescription)
                            .WithSample(new SignSampleBuilder().Build())
                            .WithSample(new SignSampleBuilder().Build())
                            .Build();
            return sign;
        }

        private void MustAddSignToRepositoryAndSaveChanges(Sign sign)
        {
            repositoryMock.Verify(r => r.Add(sign));
            repositoryMock.Verify(r => r.SaveChanges());
        }

        private void MustMergeSamplesAndSaveChanges(Sign oldSign, Sign newSign)
        {
            repositoryMock.Verify(r => r.Add(It.IsAny<Sign>()), Times.Never);
            repositoryMock.Verify(r => r.SaveChanges());
            oldSign.Samples.Count.Should().Be(4);
            oldSign.Samples.Should().Contain(newSign.Samples);
        }
    }
}
