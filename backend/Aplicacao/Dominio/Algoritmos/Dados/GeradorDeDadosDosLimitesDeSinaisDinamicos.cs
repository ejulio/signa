using System.Collections.Generic;
using System.Linq;
using Aplicacao.Dominio.Algoritmos.Caracteristicas;
using Aplicacao.Dominio.Sinais;

namespace Aplicacao.Dominio.Algoritmos.Dados
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

                    saidas.AddLast(sinal.Id);
                    saidas.AddLast(sinal.Id);
                }
                
                QuantidadeDeClasses++;
            }

            Entradas = entradas.ToArray();
            Saidas = saidas.ToArray();
        }
    }
}