using Dominio.Sinais;
using Dominio.Sinais.Caracteristicas;
using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Matematica;
using Dominio.Util;

namespace Dominio.Algoritmos.Caracteristicas
{
    public abstract class GeradorDeCaracteristicasDeFrame
    {
        protected double[] ExtrairCaracteristicasDoFrame(Frame frame)
        {
            return ExtrairCaracteristicasDaMao(frame.MaoEsquerda)
                .Concat(ExtrairCaracteristicasDaMao(frame.MaoDireita))
                .ToArray();    
        }

        private IEnumerable<double> ExtrairCaracteristicasDaMao(Mao mao)
        {
            var direcaoDosDedos = mao.Dedos.Select(d => d.Direcao.Normalizado()).ToArray().Concatenar();
            return mao.VetorNormalDaPalma.Normalizado()
                .Concat(mao.Direcao.Normalizado())
                .Concat(AngulosEntreDedosEPalmaDaMao(mao).Normalizado())
                .Concat(AngulosEntreDedos(mao).Normalizado())
                .Concat(direcaoDosDedos);
        }

        private double[] AngulosEntreDedosEPalmaDaMao(Mao mao)
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

        private double[] AngulosEntreDedos(Mao mao)
        {
            double[] angulos = new double[mao.Dedos.Length - 1];

            for (int i = 0; i < angulos.Length; i++)
            {
                var posicaoDaPontaDoDedo1 = mao.Dedos[i].Direcao;
                var posicaoDaPontaDoDedo2 = mao.Dedos[i + 1].Direcao;

                angulos[i] = posicaoDaPontaDoDedo1.AnguloAte(posicaoDaPontaDoDedo2);
            }
            return angulos;
        }
    }
}