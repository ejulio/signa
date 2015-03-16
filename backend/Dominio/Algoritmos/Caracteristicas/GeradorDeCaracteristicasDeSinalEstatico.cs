using System.Collections.Generic;
using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dominio.Algoritmos.Caracteristicas
{
    public class GeradorDeCaracteristicasDeSinalEstatico : GeradorDeCaracteristicasDeFrame, IGeradorDeCaracteristicasDeSinalEstatico
    {
        public double[] ExtrairCaracteristicasDaAmostra(IList<Frame> frames)
        {
            var frame = frames[0];
            return ExtrairCaracteristicasDoFrame(frame);
        }
    }
}