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
            double[] angulo = new double[5];

            for (int i = 0; i < angulo.Length; i++)
            {
                var p1 = mao.Dedos[i].PosicaoDaPonta;
                var p2 = mao.PosicaoDaPalma;

                angulo[i] = AnguloEntre(p1, p2);
            }

            return mao.VetorNormalDaPalma
                .Concat(mao.Direcao)
                //.Concat(mao.Dedos.Select(ExtrairCaracteristicasDoDedo).Concatenar())
                .Concat(angulo);
        }

        private double AnguloEntre(double[] vetor1, double[] vetor2)
        {
            var magnitude = (Magnitude(vetor1) * Magnitude(vetor2));

            if (magnitude == 0.0)
                return 0;

            return Math.Acos(Produto(vetor1, vetor2) / magnitude);
        }

        private double Produto(double[] vetor1, double[] vetor2)
        {
            double[] resultado = new double[3];

            for (int i = 0; i < resultado.Length; i++)
            {
                resultado[i] = vetor1[i]*vetor2[i];
            }

            return resultado[0] + resultado[1] + resultado[2];
        }

        private double Magnitude(double[] vetor)
        {
            var x = vetor[0]*vetor[0];
            var y = vetor[1]*vetor[1];
            var z = vetor[2]*vetor[2];
            return Math.Sqrt(x + y + z);
        }

        private IEnumerable<double> ExtrairCaracteristicasDoDedo(Dedo dedo)
        {
            var tipo = new double[] { (int)dedo.Tipo, dedo.Apontando ? 1.0 : 0.0 };
            return tipo.Concat(dedo.Direcao);
        }
    }
}