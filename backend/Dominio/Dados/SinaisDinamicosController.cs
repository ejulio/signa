using Dominio.Algoritmos;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Dados.Repositorio;
using Dominio.Sinais;
using System.Collections.Generic;

namespace Dominio.Dados
{
    public class SinaisDinamicosController : SinaisController
    {
        private readonly IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas;
        private readonly IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos;

        public SinaisDinamicosController(IRepositorio<Sinal> repositorio,
            IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas,
            IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisDinamicos,
            IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos)
            : base(repositorio, algoritmoDeReconhecimentoDeSinaisDinamicos)
        {
            this.geradorDeCaracteristicas = geradorDeCaracteristicas;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
        }

        public int ReconhecerPrimeiroFrame(IList<Frame> amostra)
        {
            geradorDeCaracteristicas.PrimeiroFrame = null;
            geradorDeCaracteristicas.TipoFrame = TipoFrame.Primeiro;
            return algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostra);
        }

        public int ReconhecerUltimoFrame(IList<Frame> amostraPrimeiroFrame, IList<Frame> amostraUltimoFrame)
        {
            geradorDeCaracteristicas.PrimeiroFrame = amostraPrimeiroFrame[0];
            geradorDeCaracteristicas.TipoFrame = TipoFrame.Ultimo;
            return algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostraUltimoFrame);
        }
    }
}