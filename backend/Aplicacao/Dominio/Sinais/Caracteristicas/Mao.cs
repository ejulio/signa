
namespace Signa.Dominio.Sinais.Caracteristicas
{
    public class Mao
    {
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
        }
    }
}