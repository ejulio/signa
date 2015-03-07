using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Domain.Signs.Static;
using Signa.Tests.Common.Builders.Domain.Signs;
using Signa.Tests.Common.Builders.Domain.Signs.Static;
using System;
using System.Collections.Generic;
using System.IO;
using Signa.Dados.Repositorio;

namespace Signa.Tests.Integration.Data.Repository
{
    [TestClass]
    public class StaticSignRepositoryTest
    {
        private RepositorioSinaisEstaticos signRepositorioSinaisEstaticos;
        private const string SamplesFilePath = "JsonTestData/static-test-samples.json";
        private const string DescriptionTemplate = "Static sign sample {0}";
        private const string PathTemplate = "static-sample-{0}.json";

        [TestInitialize]
        public void TestInitialize()
        {
            signRepositorioSinaisEstaticos = new RepositorioSinaisEstaticos(SamplesFilePath);
        }

        [TestMethod]
        public void loading_samples_from_file()
        {
            var signs = GivenSomeSignsInTheSamplesFile();

            signRepositorioSinaisEstaticos.Carregar();

            MustHaveTheSignsOfTheFile(signs);
        }

        [TestMethod]
        public void getting_a_sign_by_id()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            signRepositorioSinaisEstaticos.Carregar();

            MustBeAbleToGetSignsByDescriptionAsId(signs);
        }

        [TestMethod]
        public void adding_sign_do_not_save_to_file_before_saving_changes()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            signRepositorioSinaisEstaticos.Carregar();

            var samplesFileContent = GetSamplesFileContent();
            
            var sign = GivenANewSign("New sign");
            signRepositorioSinaisEstaticos.Adicionar(sign);

            GetSamplesFileContent().Should().Be(samplesFileContent);

            signRepositorioSinaisEstaticos.SalvarAlteracoes();

            GetSamplesFileContent().Should().NotBe(samplesFileContent);
            GetSamplesFileContent().Length.Should().BeGreaterThan(samplesFileContent.Length);
        }

        [TestMethod]
        public void adding_sign_must_be_able_to_get_sign_by_index_and_by_id()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            signRepositorioSinaisEstaticos.Carregar();

            var signDescription = "New sign";
            var signIndex = signs.Count;

            var sign = GivenANewSign(signDescription);
            signRepositorioSinaisEstaticos.Adicionar(sign);

            signRepositorioSinaisEstaticos.Quantidade.Should().Be(signs.Count + 1);
            signRepositorioSinaisEstaticos.BuscarPorId(signDescription).Description.Should().Be(signDescription);
            signRepositorioSinaisEstaticos.BuscarPorIndice(signIndex).Description.Should().Be(signDescription);
        }

        [TestMethod]
        public void enumerating_return_items_like_GetByIndex()
        {
            GivenSomeSignsInTheSamplesFile();
            
            signRepositorioSinaisEstaticos.Carregar();

            int index = 0;

            foreach (var sign in signRepositorioSinaisEstaticos)
            {
                sign.Should().Be(signRepositorioSinaisEstaticos.BuscarPorIndice(index));
                index++;
            }
        }

        [TestMethod]
        public void loading_an_empty_file_should_not_throw_an_exception()
        {
            GivenAnEmptySamplesFile();

            Action loadCall = () => signRepositorioSinaisEstaticos.Carregar();
            Action getByIndexCall = () => signRepositorioSinaisEstaticos.BuscarPorIndice(0);
            Action getByIdCall = () => signRepositorioSinaisEstaticos.BuscarPorId("");

            loadCall.ShouldNotThrow();
            getByIndexCall.ShouldNotThrow();
            getByIdCall.ShouldNotThrow();
        }

        [TestMethod]
        public void loading_when_file_does_not_exist()
        {
            GivenThatTheSamplesFileDoesNotExist();

            Action loadCall = () => signRepositorioSinaisEstaticos.Carregar();

            loadCall.ShouldNotThrow();
        }

        [TestMethod]
        public void saving_changes_when_file_does_not_exist()
        {
            GivenThatTheSamplesFileDoesNotExist();

            var sign = GivenANewSign("saving sign");
            signRepositorioSinaisEstaticos.Adicionar(sign);

            Action loadCall = () => signRepositorioSinaisEstaticos.SalvarAlteracoes();

            loadCall.ShouldNotThrow();
            File.Exists(SamplesFilePath).Should().BeTrue();
            GetSamplesFileContent().Should().NotBe("");
        }

        private static void GivenThatTheSamplesFileDoesNotExist()
        {
            if (File.Exists(SamplesFilePath))
                File.Delete(SamplesFilePath);
        }

        private void GivenAnEmptySamplesFile()
        {
            using (StreamWriter writer = new StreamWriter(SamplesFilePath))
            {
                writer.Write("");
            }
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

        private static SinalEstatico GivenANewSign(string description)
        {
            var sign = new SinalBuilder()
                            .ComDescricao(description)
                            .WithPath("new-sign.json")
                            .ComAmostra(new AmostraBuilder().Construir())
                            .Construir();
            return sign;
        }

        private void MustBeAbleToGetSignsByDescriptionAsId(ICollection<SinalEstatico> signs)
        {
            string signId;
            for (int i = 0; i < signs.Count; i++)
            {
                signId = String.Format(DescriptionTemplate, i);
                signRepositorioSinaisEstaticos.BuscarPorId(signId).Description.Should().Be(signId);
            }
        }

        private void MustHaveTheSignsOfTheFile(ICollection<SinalEstatico> signs)
        {
            signRepositorioSinaisEstaticos.Quantidade.Should().Be(signs.Count);
            for (int i = 0; i < signs.Count; i++)
            {
                signRepositorioSinaisEstaticos
                    .BuscarPorIndice(i)
                    .Should()
                    .Match<SinalEstatico>(sign =>
                        sign.Description == String.Format(DescriptionTemplate, i) &&
                        sign.ExampleFilePath == String.Format(PathTemplate, i) &&
                        sign.Amostras.Count == 4);
            }
        }
        private string GetSamplesFileContent()
        {
            using (StreamReader reader = new StreamReader(SamplesFilePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
