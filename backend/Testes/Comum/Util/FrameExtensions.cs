using Dominio.Sinais;
using Dominio.Util;
using System.Linq;
using System.Collections.Generic;
using Dominio.Sinais.Frames;
using Dominio.Sinais.Maos;
using Dominio.Util.Matematica;

namespace Testes.Comum.Util
{
    public static class FrameExtensions
    {
        public static double[] MontarArrayEsperadoParaSinaisEstaticos(this Frame frame, TipoFrame? tipoFrame = null)
        {
            return MontarArrayEsperadoParaAMao(frame.MaoEsquerda)
                .Concat(MontarArrayEsperadoParaAMao(frame.MaoDireita))
                .ToArray();
        }

        private static double[] MontarArrayEsperadoParaAMao(Mao mao)
        {
            var direcaoDosDedos = mao.Dedos.Select(d => d.Direcao.Normalizado()).ToArray().Concatenar();
            return mao.VetorNormalDaPalma.Normalizado()
                .Concat(mao.Direcao.Normalizado())
                .Concat(AngulosProjetados(mao))
                .Concat(direcaoDosDedos)
                .ToArray();
        }

        private static IEnumerable<double> AngulosProjetados(Mao mao)
        {
            var angulos = new double[mao.Dedos.Length];

            for (int i = 0; i < angulos.Length; i++)
            {
                angulos[i] = mao.Dedos[i].PosicaoDaPonta
                    .Subtrair(mao.PosicaoDaPalma)
                    .ProjetadoNoPlano(mao.VetorNormalDaPalma)
                    .AnguloAte(mao.Direcao);

            }

            return angulos.Normalizado();
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