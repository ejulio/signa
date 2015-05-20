using Dominio.Sinais;
using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface ICaracteristicasSinalEstatico
    {
        double[] DaAmostra(IList<Frame> frames);
    }
}