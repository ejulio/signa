using System.Linq;
using Signa.Util;

namespace Signa.Dominio.Sinais.Caracteristicas
{
    public class Mao
    {
        public double[] VetorNormalDaPalma { get; set; }
        public double[] DirecaoDaMao { get; set; }

        public Dedo[] Dedos { get; set; }

        public double[] ToArray()
        {
            var fingersData = Dedos.Select(f => f.ToArray()).Concatenar();
            
            return VetorNormalDaPalma
                .Concat(DirecaoDaMao)
                .Concat(fingersData)
                .ToArray();
        }
        public static Mao Vazia()
        {
            return new Mao
            {
                Dedos = new [] { Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty() },
                DirecaoDaMao = new[] { 0.0, 0.0, 0.0 },
                VetorNormalDaPalma = new[] { 0.0, 0.0, 0.0 }
            };
        }

    }
}