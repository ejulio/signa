using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Data;
using Signa.Util;
using System.IO;

namespace Signa.Tests.Integration.Data
{
    [TestClass]
    public class SignControllerTest
    {
        private const string samplesFilePath = "JsonTestData/test-samples.json";
        private SignRepository repository;
        private StaticSignController _staticSignController;

        [TestInitialize]
        public void Setup()
        {
            repository = new SignRepository(samplesFilePath);
            _staticSignController = new StaticSignController(repository);
        }

        [TestCleanup]
        public void DeleteFiles()
        {
            if (Directory.Exists(StaticSignController.SamplesDirectory))
            {
                Directory.Delete(StaticSignController.SamplesDirectory, true);
                Directory.CreateDirectory(StaticSignController.SamplesDirectory);
            }
        }

        [TestMethod]
        public void creating_sample_file_with_CreateSampleFileIfNotExists()
        {
            const string signDescription = "new sign";
            const string fileData = "file data";

            var createdFilePath = _staticSignController.CreateSampleFileIfNotExists(signDescription, fileData);

            MustCreateFileWithContent(createdFilePath, signDescription, fileData);
        }

        [TestMethod]
        public void CreateSampleFileIfNotExists_does_not_replace_the_file_if_exists()
        {
            const string oldSignDescription = "old sign";
            const string oldFileData = "Old file data";
            const string newFileData = "New file data";

            GivenAnExistingSampleFile(oldSignDescription, oldFileData);

            _staticSignController.CreateSampleFileIfNotExists(oldSignDescription, newFileData);

            MustNotChangeFileContent(oldSignDescription, oldFileData);
        }

        private void GivenAnExistingSampleFile(string oldSignDescription, string oldFileData)
        {
            _staticSignController.CreateSampleFileIfNotExists(oldSignDescription, oldFileData);
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
