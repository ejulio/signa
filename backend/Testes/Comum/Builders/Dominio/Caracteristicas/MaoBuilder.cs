using Signa.Dominio.Sinais.Caracteristicas;

namespace Testes.Comum.Builders.Dominio.Caracteristicas
{
    public class MaoBuilder
    {
        private Dedo[] dedos;
        private double[] vetorNormalDaPalma;
        private double[] direcaoDaMao;

        public MaoBuilder()
        {
            dedos = DedoBuilder.DedosPadroes();
            vetorNormalDaPalma = new ArrayDeDouble().ComTamanho(3).Construir();
            direcaoDaMao = new ArrayDeDouble().ComTamanho(3).Construir();
        }

        public MaoBuilder ComDedos(Dedo[] dedos)
        {
            this.dedos = dedos;
            return this;
        }

        public MaoBuilder ComVetorNormalDaPalma(double[] vetorNormalDaPalma)
        {
            this.vetorNormalDaPalma = vetorNormalDaPalma;
            return this;
        }

        public MaoBuilder ComDirecaoDaMao(double[] direcaoDaMao)
        {
            this.direcaoDaMao = direcaoDaMao;
            return this;
        }

        public MaoBuilder ParaOIndice(int indice)
        {
            vetorNormalDaPalma = new double[] { indice, indice, indice };
            direcaoDaMao = new double[] { indice, indice, indice };
            var direcaoDosDedos = new double[] {indice, indice, indice};

            dedos = new[]
            {
                new DedoBuilder().DoTipo(TipoDeDedo.Dedao).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Indicador).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Meio).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Anelar).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Mindinho).ComDirecao(direcaoDosDedos).Construir()
            };

            return this;
        }

        public Mao Construir()
        {
            return new Mao
            {
                Dedos = dedos,
                PalmNormal = vetorNormalDaPalma,
                HandDirection = direcaoDaMao
            };
        }
    }
}
