using Dominio.Sinais;
using System.Collections.Generic;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalDinamico
    {
        double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}