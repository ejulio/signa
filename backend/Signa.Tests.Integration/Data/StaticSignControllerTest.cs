using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Data;
using Signa.Data.Repository;
using Signa.Util;
using System.IO;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Signs.Static;

namespace Signa.Tests.Integration.Data
{
    [TestClass]
    public class StaticSignControllerTest
    {
        private const string samplesFilePath = "JsonTestData/test-samples.json";
        private IRepository<Domain.Signs.Static.Sign> repository;
        private StaticSignController staticSignController;

        [TestInitialize]
        public void Setup()
        {
            repository = new StaticSignRepository(samplesFilePath);
            staticSignController = new StaticSignController(repository, null);

            Directory.CreateDirectory(StaticSignController.SamplesDirectory);
        }

        [TestCleanup]
        public void DeleteFiles()
        {
            if (Directory.Exists(StaticSignController.SamplesDirectory))
            {
                Directory.Delete(StaticSignController.SamplesDirectory, true);
            }
        }

        [TestMethod]
        public void creating_the_sign_sample_file()
        {
            const string signDescription = "new sign";
            const string fileData = "file data";

            var createdFilePath = staticSignController.CreateSampleFileIfNotExists(signDescription, fileData);

            MustCreateFileWithContent(createdFilePath, signDescription, fileData);
        }

        [TestMethod]
        public void not_replacing_and_existing_file()
        {
            const string oldSignDescription = "old sign";
            const string oldFileData = "Old file data";
            const string newFileData = "New file data";

            GivenAnExistingSampleFile(oldSignDescription, oldFileData);

            staticSignController.CreateSampleFileIfNotExists(oldSignDescription, newFileData);

            MustNotChangeFileContent(oldSignDescription, oldFileData);
        }

        [TestMethod]
        public void saving_sign()
        {
            var sample = new SampleBuilder().Build();
            const string signDescription = "New Sign";
            const string fileContent = "New sign file content";

            staticSignController.Save(signDescription, fileContent, sample);

            var filePath = StaticSignController.SamplesDirectory + signDescription.Underscore() + ".json";
            MustCreateFileWithContent(filePath, signDescription, fileContent);

            var sign = repository.GetById(signDescription);
            sign.Should().NotBeNull();
            sign.Samples.Should().Contain(sample);
        }

        private void GivenAnExistingSampleFile(string oldSignDescription, string oldFileData)
        {
            staticSignController.CreateSampleFileIfNotExists(oldSignDescription, oldFileData);
        }

        private static void MustCreateFileWithContent(string createdFilePath, string signDescription, string fileData)
        {
            var file = StaticSignController.SamplesDirectory + signDescription.Underscore() + ".json";
            createdFilePath.Should().Be(file);
            File.Exists(file).Should().BeTrue();
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadToEnd().Should().Be(fileData);
            }
        }

        private static void MustNotChangeFileContent(string signDescription, string oldFileData)
        {
            var file = StaticSignController.SamplesDirectory + signDescription.Underscore() + ".json";
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadToEnd().Should().Be(oldFileData);
            }
        }
    }
}
