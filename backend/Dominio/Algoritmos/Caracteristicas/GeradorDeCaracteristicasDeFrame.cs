using System.Linq;
using Aplicacao.Dominio.Sinais;
using Aplicacao.Dominio.Sinais.Caracteristicas;
using Aplicacao.Util;

namespace Aplicacao.Dominio.Algoritmos.Caracteristicas
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