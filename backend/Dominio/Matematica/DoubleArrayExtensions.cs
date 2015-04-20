using System;

namespace Dominio.Matematica
{
    public static class DoubleArrayExtensions
    {
        public static double Magnitude(this double[] vetor)
        {
            var xAoQuadrado = vetor[0] * vetor[0];
            var yAoQuadrado = vetor[1] * vetor[1];
            var zAoQuadrado = vetor[2] * vetor[2];
            return Math.Sqrt(xAoQuadrado + yAoQuadrado + zAoQuadrado);
        }

        public static double ProdutoCom(this double[] vetor1, double[] vetor2)
        {
            var produtoDeX = vetor1[0] * vetor2[0];
            var produtoDeY = vetor1[1] * vetor2[1];
            var produtoDeZ = vetor1[2] * vetor2[2];

            return produtoDeX + produtoDeY + produtoDeZ;
        }

        public static double AnguloAte(this double[] vetor1, double[] vetor2)
        {
            var magnitude = vetor1.Magnitude() * vetor2.Magnitude();

            if (magnitude == 0.0)
                return 0;

            return Math.Acos(vetor1.ProdutoCom(vetor2) / magnitude);
        }

        public static double[] Normalizado(this double[] vetor)
        {
            var magnitude = vetor.Magnitude();
            return new[]
            {
                vetor[0] == 0 ? 0 : vetor[0] / magnitude,
                vetor[1] == 0 ? 0 : vetor[1] / magnitude,
                vetor[2] == 0 ? 0 : vetor[2] / magnitude
            };
        }
    }
}