using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalEstatico
    {
        double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}