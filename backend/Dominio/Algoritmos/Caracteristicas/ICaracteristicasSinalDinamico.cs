using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface ICaracteristicasSinalDinamico
    {
        double[][] DaAmostra(IList<Frame> amostra);
    }
}