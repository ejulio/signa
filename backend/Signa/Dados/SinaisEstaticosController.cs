using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using Signa.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Signa.Dados
{
    public class SinaisEstaticosController
    {
        public const string CaminhoDoArquivoDoRepositorio = "./data/repositorio-sinais.json";

        public const string DiretorioDeAmostras = "exemplos/"; 

        private readonly IRepositorio<Sinal> repositorio;
        private readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos;

        public SinaisEstaticosController(IRepositorio<Sinal> repositorio, IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos)
        {
            this.repositorio = repositorio;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public void Adicionar(Sinal sinalEstatico)
        {
            Sinal sinalNoRepositorio = repositorio.BuscarPorDescricao(sinalEstatico.Descricao);
            if (sinalNoRepositorio == null)
            {
                repositorio.Adicionar(sinalEstatico);
            }
            else
            {
                sinalNoRepositorio.Amostras = sinalNoRepositorio.Amostras.Concat(sinalEstatico.Amostras).ToArray();
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

        public int Reconhecer(IList<Frame> amostra)
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