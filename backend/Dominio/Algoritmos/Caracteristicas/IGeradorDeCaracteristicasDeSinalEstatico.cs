using Dominio.Sinais;
using System.Collections.Generic;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalEstatico
    {
        double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}