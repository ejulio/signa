using Dominio.Sinais;
using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class CaracteristicasSinalEstatico : GeradorDeCaracteristicasDeFrame, ICaracteristicasSinalEstatico
    {
        public double[] DaAmostra(IList<Frame> frames)
        {
            var primeiroFrame = frames[0];
            return ExtrairCaracteristicasDoFrame(primeiroFrame);
        }
    }
}