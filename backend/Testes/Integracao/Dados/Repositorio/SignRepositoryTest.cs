using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Dados.Repositorio;
using Signa.Domain.Sinais.Dinamico;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Dinamico;

namespace Testes.Integracao.Dados.Repositorio
{
    [TestClass]
    public class SignRepositoryTest
    {
        private RepositorioDeSinaisDinamicos repositorioDeSinaisDinamicos;
        private const string SamplesFilePath = Caminhos.CaminhoDoArquivoDeDeAmostras;
        private const string DescriptionTemplate = "Sign sample {0}";
        private const string PathTemplate = "sample-{0}.json";

        [TestInitialize]
        public void TestInitialize()
        {
            repositorioDeSinaisDinamicos = new RepositorioDeSinaisDinamicos(SamplesFilePath);
        }

        [TestMethod]
        public void loading_samples_from_file()
        {
            var signs = GivenSomeSignsInTheSamplesFile();

            repositorioDeSinaisDinamicos.Carregar();

            MustHaveTheSignsOfTheFile(signs);
        }

        [TestMethod]
        public void getting_a_sign_by_id()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            repositorioDeSinaisDinamicos.Carregar();

            MustBeAbleToGetSignsByDescriptionAsId(signs);
        }

        [TestMethod]
        public void adding_sign_do_not_save_to_file_before_saving_changes()
        {
            GivenSomeSignsInTheSamplesFile();
            
            repositorioDeSinaisDinamicos.Carregar();

            var samplesFileContent = GetSamplesFileContent();
            
            var sign = GivenANewSign("New sign");
            repositorioDeSinaisDinamicos.Adicionar(sign);

            GetSamplesFileContent().Should().Be(samplesFileContent);

            repositorioDeSinaisDinamicos.SalvarAlteracoes();

            GetSamplesFileContent().Should().NotBe(samplesFileContent);
            GetSamplesFileContent().Length.Should().BeGreaterThan(samplesFileContent.Length);
        }

        [TestMethod]
        public void adding_sign_must_be_able_to_get_sign_by_index_and_by_id()
        {
            var signs = GivenSomeSignsInTheSamplesFile();
            
            repositorioDeSinaisDinamicos.Carregar();

            var signDescription = "New sign";
            var signIndex = signs.Count;

            var sign = GivenANewSign(signDescription);
            repositorioDeSinaisDinamicos.Adicionar(sign);

            repositorioDeSinaisDinamicos.Quantidade.Should().Be(signs.Count + 1);
            repositorioDeSinaisDinamicos.BuscarPorId(signDescription).Descricao.Should().Be(signDescription);
            repositorioDeSinaisDinamicos.BuscarPorIndice(signIndex).Descricao.Should().Be(signDescription);
        }

        [TestMethod]
        public void enumerating_return_items_like_GetByIndex()
        {
            GivenSomeSignsInTheSamplesFile();
            
            repositorioDeSinaisDinamicos.Carregar();

            int index = 0;

            foreach (var sign in repositorioDeSinaisDinamicos)
            {
                sign.Should().Be(repositorioDeSinaisDinamicos.BuscarPorIndice(index));
                index++;
            }
        }

        [TestMethod]
        public void loading_an_empty_file_should_not_throw_an_exception()
        {
            GivenAnEmptySamplesFile();

            Action loadCall = () => repositorioDeSinaisDinamicos.Carregar();
            Action getByIndexCall = () => repositorioDeSinaisDinamicos.BuscarPorIndice(0);
            Action getByIdCall = () => repositorioDeSinaisDinamicos.BuscarPorId("");

            loadCall.ShouldNotThrow();
            getByIndexCall.ShouldNotThrow();
            getByIdCall.ShouldNotThrow();
        }

        [TestMethod]
        public void loading_when_file_does_not_exist()
        {
            GivenThatTheSamplesFileDoesNotExist();

            Action loadCall = () => repositorioDeSinaisDinamicos.Carregar();

            loadCall.ShouldNotThrow();
        }

        [TestMethod]
        public void saving_changes_when_file_does_not_exist()
        {
            GivenThatTheSamplesFileDoesNotExist();

            var sign = GivenANewSign("saving sign");
            repositorioDeSinaisDinamicos.Adicionar(sign);

            Action loadCall = () => repositorioDeSinaisDinamicos.SalvarAlteracoes();

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

        private ICollection<SinalDinamico> GivenSomeSignsInTheSamplesFile()
        {
            var signs = new ColecaoDeSinaisDinamicosBuilder()
                            .ComTamanho(4)
                            .ComTemplateDeDescricao(DescriptionTemplate)
                            .ComTemplateDeCaminho(PathTemplate)
                            .Construir();

            var json = JsonConvert.SerializeObject(signs);

            using (StreamWriter writer = new StreamWriter(SamplesFilePath))
            {
                writer.Write(json);
            }

            return signs;
        }

        private static SinalDinamico GivenANewSign(string description)
        {
            var sign = new SinalBuilder()
                            .WithDescription(description)
                            .WithPath("new-sign.json")
                            .ComAmostra(new AmostraDeSinalBuilder().Construir())
                            .Construir();
            return sign;
        }

        private void MustBeAbleToGetSignsByDescriptionAsId(ICollection<SinalDinamico> signs)
        {
            string signId;
            for (int i = 0; i < signs.Count; i++)
            {
                signId = String.Format(DescriptionTemplate, i);
                repositorioDeSinaisDinamicos.BuscarPorId(signId).Descricao.Should().Be(signId);
            }
        }

        private void MustHaveTheSignsOfTheFile(ICollection<SinalDinamico> signs)
        {
            repositorioDeSinaisDinamicos.Quantidade.Should().Be(signs.Count);
            for (int i = 0; i < signs.Count; i++)
            {
                repositorioDeSinaisDinamicos
                    .BuscarPorIndice(i)
                    .Should()
                    .Match<SinalDinamico>(sign =>
                        sign.Descricao == String.Format(DescriptionTemplate, i) &&
                        sign.CaminhoParaArquivoDeExemplo == String.Format(PathTemplate, i) &&
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
