
namespace Dominio.Sinais.Caracteristicas
{
    public class Mao
    {
        public double RaioDaEsfera { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
        public double Yaw { get; set; }

        private double[] posicaoDaPalma;
        public double[] PosicaoDaPalma
        {
            get { return posicaoDaPalma; }
            set { posicaoDaPalma = value ?? new[] { 0.0, 0.0, 0.0 }; }
        }

        private double[] velocidadeDaPalma;

        public double[] VelocidadeDaPalma
        {
            get { return velocidadeDaPalma; }
            set { velocidadeDaPalma = value ?? new[] {0.0, 0.0, 0.0}; }
        }

        private double[] vetorNormalDaPalma;
        public double[] VetorNormalDaPalma
        {
            get { return vetorNormalDaPalma; }
            set { vetorNormalDaPalma = value ?? new[] {0.0, 0.0, 0.0}; }
        }

        private double[] direcao;
        public double[] Direcao
        {
            get { return direcao; }
            set { direcao = value ?? new[] { 0.0, 0.0, 0.0 }; }
        }

        private Dedo[] dedos;
        public Dedo[] Dedos
        {
            get { return dedos; }
            set { dedos = value ?? new[] { new Dedo(), new Dedo(), new Dedo(), new Dedo(), new Dedo() }; }
        }

        public Mao()
        {
            Dedos = new[] { new Dedo(), new Dedo(), new Dedo(), new Dedo(), new Dedo() };
            Direcao = new[] {0.0, 0.0, 0.0};
            VetorNormalDaPalma = new[] {0.0, 0.0, 0.0};
            PosicaoDaPalma = new[] {0.0, 0.0, 0.0};
            VelocidadeDaPalma = new[] {0.0, 0.0, 0.0};
        }
    }
}