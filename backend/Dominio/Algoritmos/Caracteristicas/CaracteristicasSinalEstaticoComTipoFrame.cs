using Dominio.Sinais;
using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais.Frames;
using Dominio.Util.Matematica;

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
            double[] distanciaMaoDireita = {0.0, 0.0, 0.0};
            double[] distanciaMaoEsquerda = {0.0, 0.0, 0.0};
            double[] tipo = {(double)TipoFrame};

            if (PrimeiroFrame != null && amostra[0].MaoDireita != null && PrimeiroFrame.MaoDireita != null)
            {
                distanciaMaoDireita = PrimeiroFrame.MaoDireita.PosicaoDaPalma.Subtrair(amostra[0].MaoDireita.PosicaoDaPalma).Normalizado();
            }

            if (PrimeiroFrame != null && amostra[0].MaoEsquerda != null && PrimeiroFrame.MaoEsquerda != null)
            {
                distanciaMaoEsquerda = PrimeiroFrame.MaoEsquerda.PosicaoDaPalma.Subtrair(amostra[0].MaoEsquerda.PosicaoDaPalma).Normalizado();
            }

            return tipo
                .Concat(distanciaMaoDireita)
                .Concat(distanciaMaoEsquerda)
                .Concat(caracteristicas.DaAmostra(amostra))
                .ToArray();
        }
    }
}