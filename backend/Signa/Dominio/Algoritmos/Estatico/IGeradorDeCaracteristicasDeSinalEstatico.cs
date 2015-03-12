using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IGeradorDeCaracteristicasDeSinalEstatico
    {
        double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}