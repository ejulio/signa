using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Domain.Sinais.Estatico;
using Signa.Testes.Comum.Builders.Dominio.Sinais.Estatico;
using Signa.Util;
using System.IO;

namespace Signa.Testes.Integracao.Dados
{
    [TestClass]
    public class StaticSignControllerTest
    {
        private const string SamplesFilePath = Caminhos.CaminhoDoArquivoDeDeAmostras;
        private IRepositorio<SinalEstatico> repositorio;
        private SinaisEstaticosController sinaisEstaticosController;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new RepositorioSinaisEstaticos(SamplesFilePath);
            sinaisEstaticosController = new SinaisEstaticosController(repositorio, null);

            Directory.CreateDirectory(SinaisEstaticosController.SamplesDirectory);
        }

        [TestCleanup]
        public void DeleteFiles()
        {
            if (Directory.Exists(SinaisEstaticosController.SamplesDirectory))
            {
                Directory.Delete(SinaisEstaticosController.SamplesDirectory, true);
            }
        }

        [TestMethod]
        public void creating_the_sign_sample_file()
        {
            const string signDescription = "new sign";
            const string fileData = "file data";

            var createdFilePath = sinaisEstaticosController.CreateSampleFileIfNotExists(signDescription, fileData);

            MustCreateFileWithContent(createdFilePath, signDescription, fileData);
        }

        [TestMethod]
        public void not_replacing_and_existing_file()
        {
            const string oldSignDescription = "old sign";
            const string oldFileData = "Old file data";
            const string newFileData = "New file data";

            GivenAnExistingSampleFile(oldSignDescription, oldFileData);

            sinaisEstaticosController.CreateSampleFileIfNotExists(oldSignDescription, newFileData);

            MustNotChangeFileContent(oldSignDescription, oldFileData);
        }

        [TestMethod]
        public void saving_sign()
        {
            var sample = new AmostraBuilder().Construir();
            const string signDescription = "New Sign";
            const string fileContent = "New sign file content";

            sinaisEstaticosController.Save(signDescription, fileContent, sample);

            var filePath = SinaisEstaticosController.SamplesDirectory + signDescription.Underscore() + ".json";
            MustCreateFileWithContent(filePath, signDescription, fileContent);

            var sign = repositorio.BuscarPorId(signDescription);
            sign.Should().NotBeNull();
            sign.Amostras.Should().Contain(sample);
        }

        private void GivenAnExistingSampleFile(string oldSignDescription, string oldFileData)
        {
            sinaisEstaticosController.CreateSampleFileIfNotExists(oldSignDescription, oldFileData);
        }

        private static void MustCreateFileWithContent(string createdFilePath, string signDescription, string fileData)
        {
            var file = SinaisEstaticosController.SamplesDirectory + signDescription.Underscore() + ".json";
            createdFilePath.Should().Be(file);
            File.Exists(file).Should().BeTrue();
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadToEnd().Should().Be(fileData);
            }
        }

        private static void MustNotChangeFileContent(string signDescription, string oldFileData)
        {
            var file = SinaisEstaticosController.SamplesDirectory + signDescription.Underscore() + ".json";
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadToEnd().Should().Be(oldFileData);
            }
        }
    }
}
