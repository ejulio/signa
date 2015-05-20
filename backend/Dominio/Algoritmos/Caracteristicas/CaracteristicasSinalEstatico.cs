using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class CaracteristicasSinalEstatico : CaracteristicasFrame, ICaracteristicasSinalEstatico
    {
        public double[] DaAmostra(IList<Frame> amostra)
        {
            var primeiroFrame = amostra[0];
            return CaracteristicasDoFrame(primeiroFrame);
        }
    }
}