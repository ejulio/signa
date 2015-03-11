using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public class GeradorDeCaracteristicasDeSinalDinamico : GeradorDeCaracteristicasDeFrame
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