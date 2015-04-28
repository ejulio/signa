using Dominio.Algoritmos;
using Dominio.Dados.Repositorio;
using Dominio.Sinais;
using Dominio.Util;
using System.Collections.Generic;
using System.IO;

namespace Dominio.Dados
{
    public abstract class SinaisController
    {
        public const string CaminhoDoArquivoDoRepositorio = "./data/repositorio-sinais.json";
        public const string DiretorioDeExemplos = "exemplos/"; 

        private readonly IRepositorio<Sinal> repositorio;
        private readonly IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos;

        protected SinaisController(IRepositorio<Sinal> repositorio, IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos)
        {
            this.repositorio = repositorio;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public bool Reconhecer(int idSinal, IList<Frame> amostra)
        {
            return idSinal == algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostra);
        }

        public void SalvarAmostraDoSinal(string descricaoDoSinal, string conteudoDoArquivoDeExemplo, IList<Frame> amostra)
        {
            var nomeDoArquivo = CriarArquivoDeExemploSeNaoExistir(descricaoDoSinal, conteudoDoArquivoDeExemplo);
            Adicionar(new Sinal
            {
                Descricao = descricaoDoSinal,
                CaminhoParaArquivoDeExemplo = nomeDoArquivo,
                Amostras = new List<IList<Frame>> { amostra }
            });
        }

        private string CriarArquivoDeExemploSeNaoExistir(string descricaoDoSinal, string conteudoDoArquivoDeExemplo)
        {
            var caminhoDoArquivo = DiretorioDeExemplos + descricaoDoSinal.RemoverAcentos().Underscore() + ".json";

            if (File.Exists(caminhoDoArquivo))
                return caminhoDoArquivo;

            using (StreamWriter writer = new StreamWriter(caminhoDoArquivo))
            {
                writer.Write(conteudoDoArquivoDeExemplo);
            }

            return caminhoDoArquivo;
        }

        private void Adicionar(Sinal sinal)
        {
            Sinal sinalNoRepositorio = repositorio.BuscarPorDescricao(sinal.Descricao);
            if (sinalNoRepositorio == null)
            {
                repositorio.Adicionar(sinal);
            }
            else
            {
                sinalNoRepositorio.AdicionarAmostra(sinal.Amostras[0]);
            }
            repositorio.SalvarAlteracoes();
        }
    }
}