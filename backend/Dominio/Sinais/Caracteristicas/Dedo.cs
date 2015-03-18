
namespace Dominio.Sinais.Caracteristicas
{
    public class Dedo
    {
        private double[] posicaoDaPonta;
        public double[] PosicaoDaPonta
        {
            get { return posicaoDaPonta; }
            set { posicaoDaPonta = value ?? new[] { 0.0, 0.0, 0.0 }; }
        }
        public double[] VelocidadeDaPonta { get; set; }
        public bool Apontando { get; set; }
        public double[] Direcao { get; set; }
        public TipoDeDedo Tipo { get; set; }

        public static Dedo Empty()
        {
            return new Dedo
            {
                Tipo = TipoDeDedo.Dedao,
                Direcao = new[] { 0.0, 0.0, 0.0 },
                PosicaoDaPonta = new[] { 0.0, 0.0, 0.0 }
            };
        }
    }
}