using Dominio.Sinais.Frames;
using System.Collections.Generic;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface ICaracteristicasSinalEstatico
    {
        double[] DaAmostra(IList<Frame> amostra);
    }
}