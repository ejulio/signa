using Signa.Dominio.Sinais;
using Signa.Dominio.Sinais.Caracteristicas;
using Signa.Util;
using System.Linq;

namespace Signa.Dominio.Algoritmos.Caracteristicas
{
    public abstract class GeradorDeCaracteristicasDeFrame
    {
        protected double[] ExtrairCaracteristicasDoFrame(Frame frame)
        {
            return ExtrairCaracteristicasDaMao(frame.MaoEsquerda)
                .Concat(ExtrairCaracteristicasDaMao(frame.MaoDireita))
                .ToArray();    
        }

        private double[] ExtrairCaracteristicasDaMao(Mao mao)
        {
            var caracteristicasDosDedos = IEnumerableExtensions.Concatenar<double>(mao.Dedos.Select(ExtrairCaracteristicasDoDedo));
            return mao.VetorNormalDaPalma
                .Concat(mao.Direcao)
                .Concat(caracteristicasDosDedos)
                .ToArray();
        }

        private double[] ExtrairCaracteristicasDoDedo(Dedo dedo)
        {
            var tipo = new double[] { (int)dedo.Tipo };
            return tipo.Concat(dedo.Direcao).ToArray();
        }
    }
}