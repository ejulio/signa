using Signa.Dados.Repositorio;
using Signa.Domain.Algoritmos;
using Signa.Domain.Sinais.Estatico;
using Signa.Util;
using System.IO;
using System.Linq;

namespace Signa.Dados
{
    public class SinaisEstaticosController
    {
        public const string SignSamplesFilePath = "./data/static-sign-samples.json";

        public const string SamplesDirectory = "samples/"; 

        private IRepositorio<SinalEstatico> repositorio;
        private readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos;

        public SinaisEstaticosController(IRepositorio<SinalEstatico> repositorio, IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos)
        {
            this.repositorio = repositorio;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public void Adicionar(SinalEstatico sinalEstatico)
        {
            SinalEstatico sinalEstaticoInRepository = repositorio.BuscarPorId(sinalEstatico.Description);
            if (sinalEstaticoInRepository == null)
            {
                repositorio.Adicionar(sinalEstatico);
            }
            else
            {
                sinalEstaticoInRepository.Amostras = sinalEstaticoInRepository.Amostras.Concat(sinalEstatico.Amostras).ToArray();
            }
            repositorio.SalvarAlteracoes();
        }

        public string CreateSampleFileIfNotExists(string signDescription, string signSample)
        {
            var filePath = SamplesDirectory + signDescription.RemoveAccents().Underscore() + ".json";

            if (File.Exists(filePath))
                return filePath;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(signSample);
            }

            return filePath;
        }

        public int Reconhecer(Sample sample)
        {
            return algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(sample);
        }

        public void Save(string signDescription, string exampleFileContent, Sample sample)
        {
            var fileName = CreateSampleFileIfNotExists(signDescription, exampleFileContent);
            Adicionar(new SinalEstatico
            {
                Description = signDescription,
                ExampleFilePath = fileName,
                Amostras = new[] { sample }
            });
        }
    }
}