
namespace Dominio.Sinais.Maos
{
    public class Dedo
    {
        public bool Apontando { get; set; }
        public TipoDeDedo Tipo { get; set; }

        private double[] posicaoDaPonta;
        public double[] PosicaoDaPonta
        {
            get { return posicaoDaPonta; }
            set { posicaoDaPonta = value ?? new[] { 0.0, 0.0, 0.0 }; }
        }

        private double[] velocidadeDaPonta;
        public double[] VelocidadeDaPonta
        {
            get { return velocidadeDaPonta; }
            set { velocidadeDaPonta = value ?? new[] { 0.0, 0.0, 0.0 }; }
        }

        private double[] direcao;
        public double[] Direcao
        {
            get { return direcao; }
            set { direcao = value ?? new[] { 0.0, 0.0, 0.0 }; }
        }

        public Dedo()
        {
            Tipo = TipoDeDedo.Dedao;
            Direcao = new[] {0.0, 0.0, 0.0};
            PosicaoDaPonta = new[] {0.0, 0.0, 0.0};
            VelocidadeDaPonta = new[] {0.0, 0.0, 0.0};
        }
    }
}