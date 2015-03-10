using System.Linq;
using Signa.Dominio.Sinais;
using Signa.Dominio.Sinais.Caracteristicas;
using Signa.Util;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class GeradorDeAmostraDeSinalEstatico
    {
        public double[] ExtrairCaracteristicasDaAmostra(Frame[] frames)
        {
            var frame = frames[0];
            return ExtrairCaracteristicasDaMao(frame.MaoEsquerda)
                .Concat(ExtrairCaracteristicasDaMao(frame.MaoDireita))
                .ToArray();
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