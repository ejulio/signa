using Signa.Dominio.Algoritmos.Caracteristicas;
using Signa.Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;

namespace Signa.Dominio.Algoritmos.Estatico
{
    public class GeradorDeDadosDeSinaisEstaticos : DadosParaAlgoritmoDeReconhecimentoDeSinais, 
        IGeradorDeDadosDeSinaisEstaticos
    {
        public double[][] Entradas { get; private set; }
        
        private LinkedList<double[]> entradas;

        private GeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicas;

        public GeradorDeDadosDeSinaisEstaticos(IEnumerable<Sinal> sinais)
            : base(sinais)
        {
        }

        protected override void InicializarExtracaoDeInformacoesDosSinais()
        {
            entradas = new LinkedList<double[]>();
            geradorDeCaracteristicas = new GeradorDeCaracteristicasDeSinalEstatico();
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