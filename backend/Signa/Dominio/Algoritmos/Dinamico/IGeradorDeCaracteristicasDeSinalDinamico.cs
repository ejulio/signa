using System.Collections.Generic;
using Signa.Dominio.Sinais;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public interface IGeradorDeCaracteristicasDeSinalDinamico
    {
        double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}