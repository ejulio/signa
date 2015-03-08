using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Dados.Repositorio;
using Signa.Domain.Sinais.Estatico;
using Signa.Tests.Common.Builders.Dominio.Sinais;
using System.Collections.Generic;
using System.IO;

namespace Signa.Tests.Integration.Dados.Repositorio
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

            var factory = new RepositorioFactory(SamplesFilePath);

            var staticSignRepository = factory.CriarECarregarRepositorioDeSinaisEstaticos();

            staticSignRepository.Quantidade.Should().Be(signs.Count);
        }

        [TestMethod]
        public void receiving_the_same_repository()
        {
            var factory = new RepositorioFactory(SamplesFilePath);

            var staticSignRepository1 = factory.CriarECarregarRepositorioDeSinaisEstaticos();
            var staticSignRepository2 = factory.CriarECarregarRepositorioDeSinaisEstaticos();

            staticSignRepository1.Should().BeSameAs(staticSignRepository2);
        }

        private ICollection<SinalEstatico> GivenSomeSignsInTheSamplesFile()
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