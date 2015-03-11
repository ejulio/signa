using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class GeradorDeCaracteristicasDeSinalEstatico : GeradorDeCaracteristicasDeFrame
    {
        public double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames)
        {
            var frame = frames[0];
            return ExtrairCaracteristicasDoFrame(frame);
        }
    }
}