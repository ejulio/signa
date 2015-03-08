using Signa.Dados.Repositorio;
using Signa.Util;
using System.IO;
using System.Linq;
using Signa.Dominio.Algoritmos;
using Signa.Dominio.Sinais;

namespace Signa.Dados
{
    public class SinaisEstaticosController
    {
        public const string SignSamplesFilePath = "./data/static-sign-samples.json";

        public const string DiretorioDeAmostras = "samples/"; 

        private readonly IRepositorio<Sinal> repositorio;
        private readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos;

        public SinaisEstaticosController(IRepositorio<Sinal> repositorio, IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos)
        {
            this.repositorio = repositorio;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public void Adicionar(Sinal sinalEstatico)
        {
            Sinal sinalEstaticoInRepository = repositorio.BuscarPorDescricao(sinalEstatico.Descricao);
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

        public string CriarArquivoDeExemploSeNaoExistir(string signDescription, string signSample)
        {
            var filePath = DiretorioDeAmostras + signDescription.RemoveAccents().Underscore() + ".json";

            if (File.Exists(filePath))
                return filePath;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(signSample);
            }

            return filePath;
        }

        public int Reconhecer(Amostra sample)
        {
            return algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(sample);
        }

        public void SalvarAmostraDoSinal(string signDescription, string exampleFileContent, Amostra sample)
        {
            var fileName = CriarArquivoDeExemploSeNaoExistir(signDescription, exampleFileContent);
            Adicionar(new Sinal
            {
                Descricao = signDescription,
                CaminhoParaArquivoDeExemplo = fileName,
                Amostras = new[] { sample }
            });
        }
    }
}