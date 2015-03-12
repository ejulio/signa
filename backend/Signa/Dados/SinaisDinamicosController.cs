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
    public class SinaisDinamicosController : SinaisController
    {
        private readonly IRepositorio<Sinal> repositorio;
        private readonly GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas;
        private readonly IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos;

        public SinaisDinamicosController(IRepositorio<Sinal> repositorio, 
            GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas,
            IAlgoritmoDeReconhecimentoDeSinaisDinamicos algoritmoDeReconhecimentoDeSinaisDinamicos, 
            IAlgoritmoDeReconhecimentoDeSinaisEstaticos algoritmoDeReconhecimentoDeSinaisEstaticos)
            : base(repositorio, algoritmoDeReconhecimentoDeSinaisDinamicos)
        {
            this.repositorio = repositorio;
            this.geradorDeCaracteristicas = geradorDeCaracteristicas;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
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
    }
}