using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dados
{
    public class SinaisDinamicosController : SinaisController
    {
        private readonly GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas;
        private readonly IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos;

        public SinaisDinamicosController(IRepositorio<Sinal> repositorio, 
            GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas,
            IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisDinamicos,
            IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos)
            : base(repositorio, algoritmoDeReconhecimentoDeSinaisDinamicos)
        {
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