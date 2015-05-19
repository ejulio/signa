using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Dados
{
    public class GeradorDeDadosDeSinaisEstaticos : GeradorDeDadosParaAlgoritmoDeReconhecimentoDeSinais, 
        IGeradorDeDadosDeSinaisEstaticos
    {
        public double[][] Entradas { get; private set; }
        
        private LinkedList<double[]> entradas;

        private GeradorDeCaracteristicasDeSinalEstatico geradorDeCaracteristicas;

        public override int QuantidadeDeClassesPorSinal
        {
            get { return 1; }
        }

        public GeradorDeDadosDeSinaisEstaticos(IEnumerable<Sinal> sinais)
            : base(sinais)
        {
        }

        protected override void Inicializar(IEnumerable<Sinal> sinais)
        {
            entradas = new LinkedList<double[]>();
            geradorDeCaracteristicas = new GeradorDeCaracteristicasDeSinalEstatico();
        }

        protected override void GerarEntradasESaidasParaAmostra(IList<Frame> amostra, LinkedList<int> saidas, int identificadorDoSinal)
        {
            var caracteristicas = geradorDeCaracteristicas.ExtrairCaracteristicasDaAmostra(amostra);
            entradas.AddLast(caracteristicas);
            saidas.AddLast(identificadorDoSinal);
        }

        protected override void Finalizar()
        {
            Entradas = entradas.ToArray();
        }
    }
}