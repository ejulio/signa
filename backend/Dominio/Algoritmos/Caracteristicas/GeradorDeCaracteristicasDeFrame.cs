using Dominio.Sinais;
using Dominio.Sinais.Caracteristicas;
using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Matematica;

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
            double[] angulo = new double[5];

            for (int i = 0; i < angulo.Length; i++)
            {
                var p1 = mao.Dedos[i].PosicaoDaPonta;
                var p2 = mao.PosicaoDaPalma;

                angulo[i] = p1.AnguloAte(p2);
            }

            return mao.VetorNormalDaPalma
                .Concat(mao.Direcao)
                //.Concat(mao.Dedos.Select(ExtrairCaracteristicasDoDedo).Concatenar())
                .Concat(angulo);
        }

        private IEnumerable<double> ExtrairCaracteristicasDoDedo(Dedo dedo)
        {
            var tipo = new double[] { (int)dedo.Tipo, dedo.Apontando ? 1.0 : 0.0 };
            return tipo.Concat(dedo.Direcao);
        }
    }
}