using System.Collections.Generic;
using Dominio.Sinais;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class GeradorDeCaracteristicasDeSinalDinamico : GeradorDeCaracteristicasDeFrame, IGeradorDeCaracteristicasDeSinalDinamico
    {
        public double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames)
        {
            var caracteristicasDosFrames = new double[frames.Count][];
            for (int i = 0; i < frames.Count; i++)
            {
                caracteristicasDosFrames[i] = ExtrairCaracteristicasDoFrame(frames[i]);
            }
            return caracteristicasDosFrames;
        }
    }
}