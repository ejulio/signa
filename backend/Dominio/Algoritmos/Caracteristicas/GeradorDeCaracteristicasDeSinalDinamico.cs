using Dominio.Sinais;
using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Matematica;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class GeradorDeCaracteristicasDeSinalDinamico : GeradorDeCaracteristicasDeFrame, IGeradorDeCaracteristicasDeSinalDinamico
    {
        public double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames)
        {
            var caracteristicasDosFrames = new double[frames.Count][];
            double[] posicaoDaMaoDireitaNoPrimeiroFrame = new[] { 0.0, 0.0, 0.0 };
            double[] posicaoDaMaoEsquerdaNoPrimeiroFrame = new[] { 0.0, 0.0, 0.0 };
            double[] caracteristicas;
            for (int i = 0; i < frames.Count; i++)
            {
                caracteristicas = ExtrairCaracteristicasDoFrame(frames[i]);
                if (i == 0)
                {
                    if (frames[i].MaoDireita != null)
                    {
                        posicaoDaMaoDireitaNoPrimeiroFrame = frames[i].MaoDireita.PosicaoDaPalma;
                    }
                    if (frames[i].MaoEsquerda != null)
                    {
                        posicaoDaMaoEsquerdaNoPrimeiroFrame = frames[i].MaoEsquerda.PosicaoDaPalma;
                    }
                    caracteristicas = caracteristicas.Concat(new[] { 0.0, 0.0, 0.0 }).Concat(new[] { 0.0, 0.0, 0.0 }).ToArray();
                }
                else
                {
                    if (frames[i].MaoDireita != null)
                    {
                        caracteristicas = caracteristicas.Concat(frames[i].MaoDireita.PosicaoDaPalma.Subtrair(posicaoDaMaoDireitaNoPrimeiroFrame).Normalizado()).ToArray();
                    }
                    else
                    {
                        caracteristicas = caracteristicas.Concat(new[] { 0.0, 0.0, 0.0 }).ToArray();
                    }
                    if (frames[i].MaoEsquerda != null)
                    {
                        caracteristicas = caracteristicas.Concat(frames[i].MaoEsquerda.PosicaoDaPalma.Subtrair(posicaoDaMaoEsquerdaNoPrimeiroFrame).Normalizado()).ToArray();
                    }
                    else
                    {
                        caracteristicas = caracteristicas.Concat(new[] { 0.0, 0.0, 0.0 }).ToArray();
                    }
                }
                caracteristicasDosFrames[i] = caracteristicas;
            }
            return caracteristicasDosFrames;
        }
    }
}