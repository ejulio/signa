using Dominio.Sinais.Frames;
using Dominio.Util.Matematica;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class CaracteristicasSinalEstaticoComTipoFrame : ICaracteristicasSinalEstaticoComTipoFrame
    {
        private readonly ICaracteristicasSinalEstatico caracteristicas;

        public CaracteristicasSinalEstaticoComTipoFrame(ICaracteristicasSinalEstatico caracteristicas)
        {
            this.caracteristicas = caracteristicas;
        }

        public Frame PrimeiroFrame { get; set; }

        public TipoFrame TipoFrame { get; set; }
        public double[] DaAmostra(IList<Frame> amostra)
        {
            double[] tipo = {(double)TipoFrame};

            return tipo
                .Concat(DistanciaMaoDireita(amostra[0]))
                .Concat(DistanciaMaoEsquerda(amostra[0]))
                .Concat(caracteristicas.DaAmostra(amostra))
                .ToArray();
        }

        private IEnumerable<double> DistanciaMaoDireita(Frame frame)
        {
            if (MaoDireitaExisteNoFrame(frame) && MaoDireitaExisteNoFrame(PrimeiroFrame))
            {
                return PrimeiroFrame.MaoDireita.PosicaoDaPalma
                    .Subtrair(frame.MaoDireita.PosicaoDaPalma)
                    .Normalizado();
            }

            return new[] {0.0, 0.0, 0.0};
        }

        private bool MaoDireitaExisteNoFrame(Frame frame)
        {
            return frame != null && frame.MaoDireita != null;
        }

        private IEnumerable<double> DistanciaMaoEsquerda(Frame frame)
        {
            if (MaoEsquerdaExisteNoFrame(frame) && MaoEsquerdaExisteNoFrame(PrimeiroFrame))
            {
                return PrimeiroFrame.MaoEsquerda.PosicaoDaPalma
                    .Subtrair(frame.MaoEsquerda.PosicaoDaPalma)
                    .Normalizado();
            }

            return new[] {0.0, 0.0, 0.0};
        }

        private bool MaoEsquerdaExisteNoFrame(Frame frame)
        {
            return frame != null && frame.MaoEsquerda != null;
        }
    }
}