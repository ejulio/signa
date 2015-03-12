using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public interface IGeradorDeCaracteristicasDeSinalEstatico
    {
        double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}