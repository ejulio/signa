using System.Collections.Generic;
using System.Linq;
using Dominio.Algoritmos;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Persistencia;
using Dominio.Sinais;
using Dominio.Sinais.Frames;

namespace Dominio.Gerenciamento
{
    public class SinaisDinamicosController : SinaisController
    {
        private readonly IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas;
        private readonly IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos;
        private readonly IRepositorio<Sinal> repositorio;

        public SinaisDinamicosController(IRepositorio<Sinal> repositorio,
            IGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame geradorDeCaracteristicas,
            IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisDinamicos,
            IAlgoritmoDeReconhecimentoDeSinais algoritmoDeReconhecimentoDeSinaisEstaticos)
            : base(repositorio, algoritmoDeReconhecimentoDeSinaisDinamicos)
        {
            this.geradorDeCaracteristicas = geradorDeCaracteristicas;
            this.algoritmoDeReconhecimentoDeSinaisEstaticos = algoritmoDeReconhecimentoDeSinaisEstaticos;
            this.repositorio = repositorio;
        }

        public bool ReconhecerPrimeiroFrame(int idSinal, IList<Frame> amostra)
        {
            geradorDeCaracteristicas.PrimeiroFrame = null;
            geradorDeCaracteristicas.TipoFrame = TipoFrame.Primeiro;
            return idSinal == algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostra);
        }

        public bool ReconhecerUltimoFrame(int idSinal, IList<Frame> amostraPrimeiroFrame, IList<Frame> amostraUltimoFrame)
        {
            geradorDeCaracteristicas.PrimeiroFrame = amostraPrimeiroFrame[0];
            geradorDeCaracteristicas.TipoFrame = TipoFrame.Ultimo;
            return (idSinal + repositorio.Count(s => s.Tipo == TipoSinal.Dinamico)) == algoritmoDeReconhecimentoDeSinaisEstaticos.Reconhecer(amostraUltimoFrame);
        }
    }
}