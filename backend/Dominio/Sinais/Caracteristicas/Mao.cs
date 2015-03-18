
namespace Dominio.Sinais.Caracteristicas
{
    public class Mao
    {
        private double[] posicaoDaPalma;
        public double[] PosicaoDaPalma
        {
            get { return posicaoDaPalma; }
            set { posicaoDaPalma = value ?? new[] { 0.0, 0.0, 0.0 }; }
        }
        public double[] VelocidadeDaPalma { get; set; }
        public double RaioDaEsfera { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
        public double Yaw { get; set; }



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
            set { dedos = value ?? new[] { Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty() }; }
        }

        public Mao()
        {
            Dedos = new[] {Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty(), Dedo.Empty()};
            Direcao = new[] {0.0, 0.0, 0.0};
            VetorNormalDaPalma = new[] {0.0, 0.0, 0.0};
            PosicaoDaPalma = new[] {0.0, 0.0, 0.0};
        }
    }
}