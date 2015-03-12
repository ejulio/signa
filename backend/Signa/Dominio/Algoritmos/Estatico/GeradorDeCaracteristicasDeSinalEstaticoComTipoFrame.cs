using Signa.Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame : IGeradorDeCaracteristicasDeSinalEstatico
    {
        private readonly IGeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicas;

        public GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(IGeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicas)
        {
            this.geradorDeCaracteristicas = geradorDeCaracteristicas;
        }

        public TipoFrame TipoFrame { get; set; }
        public double[] ExtrairCaracteristicasDaAmostra(IList<Frame> amostra)
        {
            var arrayTipoFrame = new[] {(double) TipoFrame};
            return arrayTipoFrame
                .Concat(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(amostra))
                .Concat(arrayTipoFrame)
                .ToArray();
        }
    }
}