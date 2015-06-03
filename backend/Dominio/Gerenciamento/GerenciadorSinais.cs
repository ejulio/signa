using Dominio.Algoritmos;
using Dominio.Persistencia;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using Dominio.Util;
using System.Collections.Generic;
using System.IO;

namespace Dominio.Gerenciamento
{
    public abstract class GerenciadorSinais
    {
        public const string CaminhoDoArquivoDoRepositorio = "./data/repositorio-sinais.json";
        public const string DiretorioDeExemplos = "exemplos/"; 

        private readonly IRepositorio<Sinal> repositorio;
        private readonly IAlgoritmoClassificacaoSinais algoritmoClassificacaoSinais;

        protected GerenciadorSinais(IRepositorio<Sinal> repositorio, IAlgoritmoClassificacaoSinais algoritmoClassificacaoSinais)
        {
            this.repositorio = repositorio;
            this.algoritmoClassificacaoSinais = algoritmoClassificacaoSinais;
        }

        public bool Reconhecer(int idSinal, IList<Frame> amostra)
        {
            return idSinal == algoritmoClassificacaoSinais.Classificar(amostra);
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
            var sinalNoRepositorio = repositorio.BuscarPorDescricao(sinal.Descricao);
            if (sinalNoRepositorio == null)
                repositorio.Adicionar(sinal);
            else
                sinalNoRepositorio.AdicionarAmostra(sinal.Amostras[0]);
            repositorio.SalvarAlteracoes();
        }
    }
}