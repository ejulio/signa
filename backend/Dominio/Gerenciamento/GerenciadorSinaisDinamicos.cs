using System.Collections.Generic;
using System.Linq;
using Dominio.Algoritmos;
using Dominio.Algoritmos.Caracteristicas;
using Dominio.Persistencia;
using Dominio.Sinais;
using Dominio.Sinais.Frames;

namespace Dominio.Gerenciamento
{
    public class GerenciadorSinaisDinamicos : GerenciadorSinais
    {
        private readonly ICaracteristicasSinalEstaticoComTipoFrame caracteristicas;
        private readonly IAlgoritmoClassificacaoSinais algoritmoClassificacaoSinaisEstaticos;
        private readonly IRepositorio<Sinal> repositorio;

        public GerenciadorSinaisDinamicos(IRepositorio<Sinal> repositorio,
            ICaracteristicasSinalEstaticoComTipoFrame caracteristicas,
            IAlgoritmoClassificacaoSinais algoritmoClassificacaoSinaisDinamicos,
            IAlgoritmoClassificacaoSinais algoritmoClassificacaoSinaisEstaticos)
            : base(repositorio, algoritmoClassificacaoSinaisDinamicos)
        {
            this.caracteristicas = caracteristicas;
            this.algoritmoClassificacaoSinaisEstaticos = algoritmoClassificacaoSinaisEstaticos;
            this.repositorio = repositorio;
        }

        public bool ReconhecerPrimeiroFrame(int idSinal, IList<Frame> amostra)
        {
            caracteristicas.PrimeiroFrame = null;
            caracteristicas.TipoFrame = TipoFrame.Primeiro;
            return idSinal == algoritmoClassificacaoSinaisEstaticos.Classificar(amostra);
        }

        public bool ReconhecerUltimoFrame(int idSinal, IList<Frame> amostraPrimeiroFrame, IList<Frame> amostraUltimoFrame)
        {
            caracteristicas.PrimeiroFrame = amostraPrimeiroFrame[0];
            caracteristicas.TipoFrame = TipoFrame.Ultimo;
            return (idSinal + repositorio.Count(s => s.Tipo == TipoSinal.Dinamico)) == algoritmoClassificacaoSinaisEstaticos.Classificar(amostraUltimoFrame);
        }
    }
}