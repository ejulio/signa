using Signa.Dominio.Algoritmos.Caracteristicas;
using Signa.Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Dominio.Algoritmos.Dinamico
{
    public class GeradorDeDadosDeSinaisDinamicos : DadosParaAlgoritmoDeReconhecimentoDeSinais, 
        IGeradorDeDadosDeSinaisDinamicos
    {
        public double[][][] Entradas { get; private set; }

        private LinkedList<double[][]> entradas;
        private GeradorDeCaracteristicasDeSinalDinamico geradorDeCaracteristicas;

        public GeradorDeDadosDeSinaisDinamicos(IEnumerable<Sinal> sinais)
            :base(sinais)
        {
        }
        protected override void InicializarExtracaoDeInformacoesDosSinais()
        {
            entradas = new LinkedList<double[][]>();
            geradorDeCaracteristicas = new GeradorDeCaracteristicasDeSinalDinamico();
        }

        protected override void GerarEntradaParaAAmostra(IList<Frame> amostra)
        {
            entradas.AddLast(geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(amostra));
        }

        protected override void FinalizarExtracaoDeInformacoesDosSinais()
        {
            Entradas = entradas.ToArray();
        }
    }
}