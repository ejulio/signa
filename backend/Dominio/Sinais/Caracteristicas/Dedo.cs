
namespace Dominio.Sinais.Caracteristicas
{
    public class Dedo
    {
        public double[] Direcao { get; set; }
        public TipoDeDedo Tipo { get; set; }

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