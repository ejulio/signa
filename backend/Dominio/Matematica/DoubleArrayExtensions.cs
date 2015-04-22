using System;
using System.Linq;

namespace Dominio.Matematica
{
    public static class DoubleArrayExtensions
    {
        public static double AnguloAte(this double[] vetor1, double[] vetor2)
        {
            var magnitude = vetor1.Magnitude() * vetor2.Magnitude();

            if (magnitude == 0.0)
                return 0;

            return Math.Acos(vetor1.ProdutoCom(vetor2) / magnitude);
        }

        public static double[] Normalizado(this double[] vetor)
        {
            var vetorNormalizado = new double[vetor.Length];
            var magnitude = vetor.Magnitude();

            if (magnitude != 0)
            {
                for (var i = 0; i < vetorNormalizado.Length; i++)
                {
                    vetorNormalizado[i] = vetor[i] / magnitude;
                }
            }

            return vetorNormalizado;
        }

        public static double Magnitude(this double[] vetor)
        {
            var produtoDoVetor = vetor.ProdutoCom(vetor);
            return Math.Sqrt(produtoDoVetor);
        }

        public static double ProdutoCom(this double[] vetor1, double[] vetor2)
        {
            double total = 0;
            for (int i = 0; i < vetor1.Length; i++)
            {
                total += vetor1[i] * vetor2[i];
            }

            return total;
        }

        public static double[] MultiplicarPor(this double[] vetor, double escalar)
        {
            return vetor.Select(valor => valor * escalar).ToArray();
        }

        public static double[] SomarCom(this double[] vetor1, double[] vetor2)
        {
            return vetor1.Select((valor, indice) => valor + vetor2[indice]).ToArray();
        }

        public static double[] Subtrair(this double[] vetor1, double[] vetor2)
        {
            return vetor1.Select((valor, indice) => valor - vetor2[indice]).ToArray();
        }

        public static double[] ProjetadoNoPlano(this double[] vetor, double[] vetorNormalDoPlano)
        {
            var vetorNormalDoPlanoNormalizado = vetorNormalDoPlano.Normalizado();
            var produto = vetor.ProdutoCom(vetorNormalDoPlanoNormalizado);
            var vetorNoPlano = vetorNormalDoPlanoNormalizado.MultiplicarPor(produto);
            return vetor.Subtrair(vetorNoPlano);
        }

        public static double[] ProjetadoEmXY(this double[] vetor)
        {
            return new[] { vetor[0], vetor[1] };
        }

        public static double[] ProjetadoEmXZ(this double[] vetor)
        {
            return new[] { vetor[0], vetor[2] };
        }

        public static double[] ProjetadoEmYZ(this double[] vetor)
        {
            return new[] { vetor[1], vetor[2] };
        }
    }
}