using Signa.Dominio.Sinais;
using Signa.Dominio.Sinais.Caracteristicas;
using Signa.Util;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public class GeradorDeAmostraDeSinalDinamico
    {
        public double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames)
        {
            var caracteristicasDosFrames = new double[frames.Count][];
            for (int i = 0; i < frames.Count; i++)
            {
                caracteristicasDosFrames[i] = ExtrairCaracteristicasDaMao(frames[i].MaoEsquerda)
                                            .Concat(ExtrairCaracteristicasDaMao(frames[i].MaoDireita))
                                            .ToArray();
            }
            return caracteristicasDosFrames;
        }

        private double[] ExtrairCaracteristicasDaMao(Mao mao)
        {
            var caracteristicasDosDedos = mao.Dedos.Select(ExtrairCaracteristicasDoMedod).Concatenar();
            return mao.VetorNormalDaPalma
                .Concat(mao.DirecaoDaMao)
                .Concat(caracteristicasDosDedos)
                .ToArray();
        }

        private double[] ExtrairCaracteristicasDoMedod(Dedo dedo)
        {
            var tipo = new double[] { (int)dedo.Tipo };
            return tipo.Concat(dedo.Direcao).ToArray();
        }
    }
}