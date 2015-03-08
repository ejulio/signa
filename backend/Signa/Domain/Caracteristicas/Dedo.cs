using System.Linq;

namespace Signa.Domain.Caracteristicas
{
    public class Dedo : IFeature
    {
        public double[] Direcao { get; set; }
        public TipoDeDedo Tipo { get; set; }

        public double[] ToArray()
        {
            var type = new double[] { (int)Tipo };
            return type.Concat(Direcao).ToArray();
        }

        public static Dedo Empty()
        {
            return new Dedo
            {
                Tipo = TipoDeDedo.Dedao,
                Direcao = new[] { 0.0, 0.0, 0.0 }
            };
        }
    }
}