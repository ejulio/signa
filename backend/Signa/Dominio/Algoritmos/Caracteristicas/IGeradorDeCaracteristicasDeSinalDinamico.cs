using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalDinamico
    {
        double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}