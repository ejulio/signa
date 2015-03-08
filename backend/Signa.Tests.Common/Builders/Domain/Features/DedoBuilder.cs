using Signa.Domain.Features;

namespace Signa.Tests.Common.Builders.Domain.Features
{
    public class DedoBuilder
    {
        private double[] direcaoDaPonta;
        private TipoDeDedo tipo;

        public DedoBuilder ComDirecao(double[] direcaoDaPonta)
        {
            this.direcaoDaPonta = direcaoDaPonta;
            return this;
        }

        public DedoBuilder DoTipo(TipoDeDedo tipo)
        {
            this.tipo = tipo;
            return this;
        }

        public Dedo Construir()
        {
            return new Dedo
            {
                Direcao = direcaoDaPonta,
                Tipo = tipo
            };
        }

        public static Dedo Dedao()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDeDedo.Dedao)
                .Construir();
        }

        public static Dedo Indicador()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDeDedo.Indicador)
                .Construir();
        }

        public static Dedo Meio()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDeDedo.Meio)
                .Construir();
        }

        public static Dedo Anelar()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDeDedo.Anelar)
                .Construir();
        }

        public static Dedo Mindinho()
        {
            return new DedoBuilder()
                .ComDirecao(DirecaoDaPonta())
                .DoTipo(TipoDeDedo.Mindinho)
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
