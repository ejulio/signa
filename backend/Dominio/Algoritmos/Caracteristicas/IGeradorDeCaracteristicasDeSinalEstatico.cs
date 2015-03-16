using Dominio.Sinais;
using System.Collections.Generic;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalEstatico
    {
        double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}