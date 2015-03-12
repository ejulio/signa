using Signa.Dominio.Sinais;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Util;

namespace Signa.Dados
{
    public class SinaisDinamicosController
    {
        public const string CaminhoDoArquivoDoRepositorio = "./data/repositorio-sinais.json";

        public const string DiretorioDeAmostras = "exemplos/"; 

        private readonly IRepositorio<Sinal> repositorio;
        private readonly GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas;
        private readonly IAlgoritmoDeReconhecimentoDeSinaisDinamicos algoritmoDeReconhecimentoDeSinaisDinamicos;
        private readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos;

        public SinaisDinamicosController(IRepositorio<Sinal> repositorio, 
            GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas,
            IAlgoritmoDeReconhecimentoDeSinaisDinamicos algoritmoDeReconhecimentoDeSinaisDinamicos, 
            IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos)
        {
            this.repositorio = repositorio;
            this.geradorDeCaracteristicas = geradorDeCaracteristicas;
            this.algoritmoDeReconhecimentoDeSinaisDinamicos = algoritmoDeReconhecimentoDeSinaisDinamicos;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public int Reconhecer(IList<Frame> amostra)
        {
            return algoritmoDeReconhecimentoDeSinaisDinamicos.Reconhecer(amostra);
        }

        public int ReconhecerPrimeiroFrame(IList<Frame> amostra)
        {
            geradorDeCaracteristicas.TipoFrame = TipoFrame.Primeiro;
            return algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostra);
        }

        public int ReconhecerUltimoFrame(IList<Frame> amostra)
        {
            geradorDeCaracteristicas.TipoFrame = TipoFrame.Ultimo;
            return algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostra);
        }

        public void SalvarAmostraDoSinal(string descricao, string conteudoDoArquivoDeExemplo, IList<Frame> amostra)
        {
            var fileName = CriarArquivoDeExemploSeNaoExistir(descricao, conteudoDoArquivoDeExemplo);
            Adicionar(new Sinal
            {
                Descricao = descricao,
                CaminhoParaArquivoDeExemplo = fileName,
                Amostras = new[] { amostra }
            });
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
    }
}