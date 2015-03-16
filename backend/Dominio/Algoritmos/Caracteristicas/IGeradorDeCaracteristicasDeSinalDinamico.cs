using System.Collections.Generic;
using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dominio.Algoritmos.Caracteristicas
{
    public interface IGeradorDeCaracteristicasDeSinalDinamico
    {
        double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames);
    }
}