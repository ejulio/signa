using Dominio.Sinais;
using Dominio.Sinais.Caracteristicas;
using Dominio.Util;
using System.Linq;
using Dominio.Matematica;

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
            return mao.VetorNormalDaPalma
                    .Concat(mao.Direcao)
                    .Concat(AngulosEntreDedosEPalmaDaMao(mao))
                    .Concat(AngulosEntreDedos(mao))
                    .ToArray();
        }

        private static double[] AngulosEntreDedosEPalmaDaMao(Mao mao)
        {
            double[] angulos = new double[mao.Dedos.Length];

            for (int i = 0; i < angulos.Length; i++)
            {
                var posicaoDaPontaDoDedo = mao.Dedos[i].PosicaoDaPonta;
                var posicaoDaPalma = mao.PosicaoDaPalma;

                angulos[i] = posicaoDaPontaDoDedo.AnguloAte(posicaoDaPalma);
            }

            return angulos;
        }

        private static double[] AngulosEntreDedos(Mao mao)
        {
            double[] angulos = new double[mao.Dedos.Length - 1];

            for (int i = 0; i < angulos.Length; i++)
            {
                var posicaoDaPontaDoDedo1 = mao.Dedos[i].PosicaoDaPonta;
                var posicaoDaPontaDoDedo2 = mao.Dedos[i + 1].PosicaoDaPonta;

                angulos[i] = posicaoDaPontaDoDedo1.AnguloAte(posicaoDaPontaDoDedo2);
            }

            return angulos;
        }
    }
}