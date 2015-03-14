
namespace Signa.Dominio.Sinais.Caracteristicas
{
    public class Mao
    {
        public double[] VetorNormalDaPalma { get; set; }
        public double[] Direcao { get; set; }

        public Dedo[] Dedos { get; set; }

        public static Mao Vazia()
        {
            return new Mao
            {
                Dedos = new [] { Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty() },
                Direcao = new[] { 0.0, 0.0, 0.0 },
                VetorNormalDaPalma = new[] { 0.0, 0.0, 0.0 }
            };
        }

    }
}