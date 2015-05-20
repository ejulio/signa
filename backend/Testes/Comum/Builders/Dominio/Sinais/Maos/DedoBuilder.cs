using Dominio.Sinais.Maos;

namespace Testes.Comum.Builders.Dominio.Sinais.Maos
{
    public class DedoBuilder
    {
        private double[] direcaoDaPonta;
        private double[] posicaoDaPonta;
        private TipoDedo tipo;

        public DedoBuilder ComDirecao(double[] direcaoDaPonta)
        {
            this.direcaoDaPonta = direcaoDaPonta;
            return this;
        }

        public DedoBuilder ComPosicaoDaPonta(double[] posicaoDaPonta)
        {
            this.posicaoDaPonta = posicaoDaPonta;
            return this;
        }

        public DedoBuilder DoTipo(TipoDedo tipo)
        {
            this.tipo = tipo;
            return this;
        }

        public Dedo Construir()
        {
            return new Dedo
            {
                Direcao = direcaoDaPonta,
                PosicaoDaPonta = posicaoDaPonta,
                Tipo = tipo
            };
        }

        public static Dedo Dedao()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDedo.Dedao)
                .Construir();
        }

        public static Dedo Indicador()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDedo.Indicador)
                .Construir();
        }

        public static Dedo Meio()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDedo.Meio)
                .Construir();
        }

        public static Dedo Anelar()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDedo.Anelar)
                .Construir();
        }

        public static Dedo Mindinho()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDedo.Mindinho)
                .Construir();
        }

        private static double[] DirecaoDaPonta()
        {
            return new ArrayDeDouble().ComTamanho(3).Construir();
        }

        public static Dedo[] DedosPadroes()
        {
            return new[] 
            { 
                DedoBuilder.Dedao(), 
                DedoBuilder.Indicador(), 
                DedoBuilder.Meio(), 
                DedoBuilder.Anelar(), 
                DedoBuilder.Mindinho() 
            };
        }
    }
}
