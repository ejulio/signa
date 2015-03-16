using Dominio.Sinais;
using Dominio.Sinais.Caracteristicas;
using Dominio.Util;
using System.Linq;

namespace Testes.Comum.Util
{
    public static class FrameExtensions
    {
        public static double[] MontarArrayEsperado(this Frame frame, TipoFrame? tipoFrame = null)
        {
            double[] arrayTipoFrame;

            if (tipoFrame.HasValue)
                arrayTipoFrame = new[] { (double)tipoFrame.Value };
            else
            {
                arrayTipoFrame = new double[0];
            }

            return arrayTipoFrame
                .Concat(MontarArrayEsperadoParaAMao(frame.MaoEsquerda))
                .Concat(MontarArrayEsperadoParaAMao(frame.MaoDireita))
                .Concat(arrayTipoFrame)
                .ToArray();
        }

        private static double[] MontarArrayEsperadoParaAMao(Mao mao)
        {
            var dadosDosDedos = mao.Dedos.Select(d =>
            {
                var tipo = new double[] { (int)d.Tipo };
                return tipo.Concat(d.Direcao).ToArray();
            }).Concatenar();

            return mao.VetorNormalDaPalma
                    .Concat(mao.Direcao)
                    .Concat(dadosDosDedos)
                    .ToArray();
        } 
    }
}