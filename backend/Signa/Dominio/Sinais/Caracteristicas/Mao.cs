using System.Linq;
using Signa.Util;

namespace Signa.Dominio.Sinais.Caracteristicas
{
    public class Mao : ICaracteristica
    {
        public double[] PalmNormal { get; set; }
        public double[] HandDirection { get; set; }

        public Dedo[] Dedos { get; set; }

        public double[] ToArray()
        {
            var fingersData = Dedos.Select(f => f.ToArray()).Concatenar();
            
            return PalmNormal
                .Concat(HandDirection)
                .Concat(fingersData)
                .ToArray();
        }
        public static Mao Vazia()
        {
            return new Mao
            {
                Dedos = new [] { Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty() },
                HandDirection = new[] { 0.0, 0.0, 0.0 },
                PalmNormal = new[] { 0.0, 0.0, 0.0 }
            };
        }

    }
}