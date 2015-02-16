using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Data;
using Signa.Model;
using Signa.Tests.Common.Builders;
using System;
using System.Collections.Generic;
using System.IO;

namespace Signa.Tests.Integration.Data
{
    [TestClass]
    public class SignRepositoryTest
    {
        private SignRepository signRepository;
        private const string samplesFilePath = "JsonTestData/test-samples.json";
        private const string descriptionTemplate = "Sign sample {0}";
        private const string pathTemplate = "sample-{0}.json";

        [TestInitialize]
        public void TestInitialize()
        {
            signRepository = new SignRepository(samplesFilePath);
        }

        [TestMethod]
        public void loading_samples_from_file()
        {
            var signs = GivenSomeSignsInTheSamplesFile();

            signRepository.Load();

            MustHaveTheSignsOfTheFile(signs);
        }

        [TestMethod]
        public void getting_a_sign_by_id()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            signRepository.Load();

            MustBeAbleToGetSignsByDescriptionAsId(signs);
        }

        [TestMethod]
        public void adding_sign_do_not_save_to_file_before_saving_changes()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            signRepository.Load();

            var samplesFileContent = GetSamplesFileContent();
            
            var sign = GivenANewSign("New sign");
            signRepository.Add(sign);

            GetSamplesFileContent().Should().Be(samplesFileContent);

            signRepository.SaveChanges();

            GetSamplesFileContent().Should().NotBe(samplesFileContent);
            GetSamplesFileContent().Length.Should().BeGreaterThan(samplesFileContent.Length);
        }

        [TestMethod]
        public void adding_sign_must_be_able_to_get_sign_by_index_and_by_id()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            signRepository.Load();

            var signDescription = "New sign";
            var signIndex = signs.Count;

            var sign = GivenANewSign(signDescription);
            signRepository.Add(sign);

            signRepository.Count.Should().Be(signs.Count + 1);
            signRepository.GetById(signDescription).Description.Should().Be(signDescription);
            signRepository.GetByIndex(signIndex).Description.Should().Be(signDescription);
        }

        [TestMethod]
        public void enumerating_return_items_like_GetByIndex()
        {
            GivenSomeSignsInTheSamplesFile();
            
            signRepository.Load();

            int index = 0;

            foreach (var sign in signRepository)
            {
                sign.Should().Be(signRepository.GetByIndex(index));
                index++;
            }
        }

        [TestMethod]
        public void loading_an_empty_file_should_not_throw_an_exception()
        {
            GivenAnEmptySamplesFile();

            Action loadCall = () => signRepository.Load();
            Action getByIndexCall = () => signRepository.GetByIndex(0);
            Action getByIdCall = () => signRepository.GetById("");

            loadCall.ShouldNotThrow();
            getByIndexCall.ShouldNotThrow();
            getByIdCall.ShouldNotThrow();
        }

        private void GivenAnEmptySamplesFile()
        {
            using (StreamWriter writer = new StreamWriter(samplesFilePath))
            {
                writer.Write("");
            }
        }

        private ICollection<Sign> GivenSomeSignsInTheSamplesFile()
        {
            var signs = new SignCollectionBuilder()
                            .WithSize(4)
                            .WithDescriptionTemplate(descriptionTemplate)
                            .WithPathTemplate(pathTemplate)
                            .Build();

            var json = JsonConvert.SerializeObject(signs);

            using (StreamWriter writer = new StreamWriter(samplesFilePath))
            {
                writer.Write(json);
            }

            return signs;
        }

        private static Sign GivenANewSign(string description)
        {
            var sign = new SignBuilder()
                            .WithDescription(description)
                            .WithPath("new-sign.json")
                            .WithSample(new SignSample
                            {
                                AnglesBetweenFingers = new DoubleArrayBuilder().WithSize(4).Build(),
                                PalmNormal = new DoubleArrayBuilder().WithSize(3).Build(),
                                HandDirection = new DoubleArrayBuilder().WithSize(3).Build()
                            })
                            .Build();
            return sign;
        }

        private void MustBeAbleToGetSignsByDescriptionAsId(ICollection<Sign> signs)
        {
            string signId;
            for (int i = 0; i < signs.Count; i++)
            {
                signId = String.Format(descriptionTemplate, i);
                signRepository.GetById(signId).Description.Should().Be(signId);
            }
        }

        private void MustHaveTheSignsOfTheFile(ICollection<Sign> signs)
        {
            signRepository.Count.Should().Be(signs.Count);
            for (int i = 0; i < signs.Count; i++)
            {
                signRepository
                    .GetByIndex(i)
                    .Should()
                    .Match<Sign>(sign =>
                        sign.Description == String.Format(descriptionTemplate, i) &&
                        sign.ExampleFilePath == String.Format(pathTemplate, i) &&
                        sign.Samples.Count == 4);
            }
        }
        private string GetSamplesFileContent()
        {
            using (StreamReader reader = new StreamReader(samplesFilePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
