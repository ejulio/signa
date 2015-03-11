using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using Signa.Util;
using System.IO;
using System.Linq;

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

        public string CriarArquivoDeExemploSeNaoExistir(string descricaoDoSinal, string conteudoDoArquivoDeExemplo)
        {
            var filePath = DiretorioDeAmostras + descricaoDoSinal.RemoverAcentos().Underscore() + ".json";

            if (File.Exists(filePath))
                return filePath;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(conteudoDoArquivoDeExemplo);
            }

            return filePath;
        }

        public int Reconhecer(Frame amostra)
        {
            return algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostra);
        }

        public void SalvarAmostraDoSinal(string descricaoDoSinal, string conteudoDoArquivoDeExemplo, Frame amostra)
        {
            var fileName = CriarArquivoDeExemploSeNaoExistir(descricaoDoSinal, conteudoDoArquivoDeExemplo);
            Adicionar(new Sinal
            {
                Descricao = descricaoDoSinal,
                CaminhoParaArquivoDeExemplo = fileName,
                Amostras = new[] { new[] { amostra } }
            });
        }
    }
}