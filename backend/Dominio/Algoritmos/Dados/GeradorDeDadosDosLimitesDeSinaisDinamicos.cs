﻿using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Dados
{
    public class GeradorDeDadosDosLimitesDeSinaisDinamicos : IGeradorDeDadosDeSinaisEstaticos
    {
        public double[][] Entradas { get; private set; }

        public int[] Saidas { get; private set; }

        public int QuantidadeDeClasses { get; private set; }

        private IEnumerable<Sinal> sinais;
        private LinkedList<double[]> entradas;
        private LinkedList<int> saidas;

        public GeradorDeDadosDosLimitesDeSinaisDinamicos(IEnumerable<Sinal> sinais)
        {
            this.sinais = sinais;
            entradas = new LinkedList<double[]>();
            saidas = new LinkedList<int>();
            ExtrairDadosDasAmostras();
        }

        private void ExtrairDadosDasAmostras()
        {
            QuantidadeDeClasses = 0;
            var geradorDeCaracteristicas = new GeradorDeCaracteristicasDeSinalEstatico();
            var geradorDeCaracteristicasComTipoFrame = new GeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(geradorDeCaracteristicas);
            Frame[] amostraDoFrame = new Frame[1];
            int indice = 0;

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                {
                    geradorDeCaracteristicasComTipoFrame.TipoFrame = TipoFrame.Primeiro;
                    amostraDoFrame[0] = amostra.First();
                    entradas.AddLast(geradorDeCaracteristicasComTipoFrame.ExtrairCaracteristicasDaAmostra(amostraDoFrame));

                    amostraDoFrame[0] = amostra.Last();
                    geradorDeCaracteristicasComTipoFrame.TipoFrame = TipoFrame.Ultimo;
                    entradas.AddLast(geradorDeCaracteristicasComTipoFrame.ExtrairCaracteristicasDaAmostra(amostraDoFrame));

                    saidas.AddLast(indice);
                    saidas.AddLast(indice);
                }
                sinal.IndiceNoAlgoritmo = indice;
                indice++;
                QuantidadeDeClasses++;
            }

            Entradas = entradas.ToArray();
            Saidas = saidas.ToArray();
        }
    }
}