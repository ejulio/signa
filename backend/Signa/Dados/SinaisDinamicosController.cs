using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;
using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Algoritmos.Estatico;

namespace Signa.Dados
{
    public class SinaisDinamicosController
    {
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
            throw new NotImplementedException("Implementar para salvar o sinal e treinar o algoritmo HMM ou HCRF");
        } 
    }
}