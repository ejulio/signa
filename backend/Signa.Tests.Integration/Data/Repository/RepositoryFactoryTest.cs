using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Data.Repository;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Signs;

namespace Signa.Tests.Integration.Data.Repository
{
    [TestClass]
    public class RepositoryFactoryTest
    {
        private const string SamplesFilePath = "JsonTestData/test-samples.json";
        private const string DescriptionTemplate = "Static sign sample {0}";
        private const string PathTemplate = "static-sample-{0}.json";

        [TestMethod]
        public void creating_and_loading_a_repository()
        {
            var signs = GivenSomeSignsInTheSamplesFile();

            var factory = new RepositoryFactory(SamplesFilePath);

            var staticSignRepository = factory.CreateAndLoadStaticSignRepository();

            staticSignRepository.Count.Should().Be(signs.Count);
        }

        [TestMethod]
        public void receiving_the_same_repository()
        {
            var factory = new RepositoryFactory(SamplesFilePath);

            var staticSignRepository1 = factory.CreateAndLoadStaticSignRepository();
            var staticSignRepository2 = factory.CreateAndLoadStaticSignRepository();

            staticSignRepository1.Should().BeSameAs(staticSignRepository2);
        }

        private ICollection<Sign> GivenSomeSignsInTheSamplesFile()
        {
            var signs = new StaticSignCollectionBuilder()
                            .WithSize(4)
                            .WithDescriptionTemplate(DescriptionTemplate)
                            .WithPathTemplate(PathTemplate)
                            .Build();

            var json = JsonConvert.SerializeObject(signs);

            using (StreamWriter writer = new StreamWriter(SamplesFilePath))
            {
                writer.Write(json);
            }

            return signs;
        }
    }
}