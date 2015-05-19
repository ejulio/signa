using Dominio.Sinais;
using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class GeradorDeCaracteristicasDeSinalEstatico : GeradorDeCaracteristicasDeFrame, IGeradorDeCaracteristicasDeSinalEstatico
    {
        public double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames)
        {
            var primeiroFrame = frames[0];
            return ExtrairCaracteristicasDoFrame(primeiroFrame);
        }
    }
}