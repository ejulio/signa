using Dominio.Sinais;
using System;
using System.Collections.Generic;

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
                foreach (var v in caracteristicasDosFrames[i])
                    if (v > 1)
                        Console.WriteLine("NORM");
            }
            return caracteristicasDosFrames;
        }
    }
}