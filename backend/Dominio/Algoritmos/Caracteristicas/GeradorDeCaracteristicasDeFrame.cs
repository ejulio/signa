using System;
using System.Collections;
using System.Collections.Generic;
using Dominio.Sinais;
using Dominio.Sinais.Caracteristicas;
using Dominio.Util;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
            double[] distanciaEntreOsDedos = new double[4];

            for (int i = 0; i < distanciaEntreOsDedos.Length; i++)
            {
                var p1 = mao.Dedos[i].PosicaoDaPonta;
                var p2 = mao.Dedos[i + 1].PosicaoDaPonta;

                var x = p1[0] - p2[0];
                var y = p1[1] - p2[1];
                var z = p1[2] - p2[2];

                distanciaEntreOsDedos[i] = x*x + y*y + z*z;
            }

            return mao.VetorNormalDaPalma
                .Concat(mao.Direcao)
                .Concat(distanciaEntreOsDedos);
        }

        private IEnumerable<double> ExtrairCaracteristicasDoDedo(Dedo dedo)
        {
            var tipo = new double[] { (int)dedo.Tipo, dedo.Apontando ? 1.0 : 0.0 };
            return tipo.Concat(dedo.Direcao);
        }
    }
}