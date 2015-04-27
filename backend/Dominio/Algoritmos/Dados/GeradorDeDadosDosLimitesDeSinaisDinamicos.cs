using Dominio.Algoritmos.Caracteristicas;
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
                    geradorDeCaracteristicasComTipoFrame.PrimeiroFrame = amostra.First();
                    int framesAmostraPrimeiroFrame = amostra.Count / 2;
                    for (int i = 0; i < framesAmostraPrimeiroFrame; i++)
                    {
                        geradorDeCaracteristicasComTipoFrame.TipoFrame = TipoFrame.Primeiro;
                        amostraDoFrame[0] = amostra[i];
                        entradas.AddLast(geradorDeCaracteristicasComTipoFrame.ExtrairCaracteristicasDaAmostra(amostraDoFrame));
                        saidas.AddLast(indice);
                    }

                    for (int i = framesAmostraPrimeiroFrame; i < amostra.Count; i++)
                    {
                        amostraDoFrame[0] = amostra[i];
                        geradorDeCaracteristicasComTipoFrame.TipoFrame = TipoFrame.Ultimo;
                        entradas.AddLast(geradorDeCaracteristicasComTipoFrame.ExtrairCaracteristicasDaAmostra(amostraDoFrame));
                        saidas.AddLast(indice + 1);
                    }
                }
                sinal.IndiceNoAlgoritmo = indice;
                indice += 2;
                QuantidadeDeClasses += 2;
            }

            Entradas = entradas.ToArray();
            Saidas = saidas.ToArray();
        }
    }
}