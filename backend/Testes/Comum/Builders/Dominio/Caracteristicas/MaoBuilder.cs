using Dominio.Sinais.Caracteristicas;

namespace Testes.Comum.Builders.Dominio.Caracteristicas
{
    public class MaoBuilder
    {
        private Dedo[] dedos;
        private double[] vetorNormalDaPalma;
        private double[] direcaoDaMao;
        private double[] posicaoDaPalma;

        public MaoBuilder()
        {
            dedos = DedoBuilder.DedosPadroes();
            vetorNormalDaPalma = new ArrayDeDouble().ComTamanho(3).Construir();
            direcaoDaMao = new ArrayDeDouble().ComTamanho(3).Construir();
            posicaoDaPalma = new ArrayDeDouble().ComTamanho(3).Construir();
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
            vetorNormalDaPalma = new double[] { indice, indice + 1, indice + 1 };
            direcaoDaMao = new double[] { indice + 1, indice, indice + 1 };
            posicaoDaPalma = new double[] { indice, indice, indice };
            var direcaoDosDedos = new double[] {indice + 1, indice, indice + 1};
            var posicaoDasPontasDosDedos = new double[] { indice + 2, indice + 2, indice + 2 };

            dedos = new[]
            {
                new DedoBuilder().DoTipo(TipoDeDedo.Dedao).ComPosicaoDaPonta(posicaoDasPontasDosDedos).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Indicador).ComPosicaoDaPonta(posicaoDasPontasDosDedos).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Meio).ComPosicaoDaPonta(posicaoDasPontasDosDedos).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Anelar).ComPosicaoDaPonta(posicaoDasPontasDosDedos).ComDirecao(direcaoDosDedos).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Mindinho).ComPosicaoDaPonta(posicaoDasPontasDosDedos).ComDirecao(direcaoDosDedos).Construir()
            };

            return this;
        }

        public Mao Construir()
        {
            return new Mao
            {
                Dedos = dedos,
                VetorNormalDaPalma = vetorNormalDaPalma,
                PosicaoDaPalma = posicaoDaPalma,
                Direcao = direcaoDaMao
            };
        }
    }
}
