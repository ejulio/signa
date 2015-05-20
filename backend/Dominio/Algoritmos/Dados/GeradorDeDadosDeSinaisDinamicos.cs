using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Dados
{
    public class GeradorDeDadosDeSinaisDinamicos : GeradorDeDadosParaAlgoritmoDeReconhecimentoDeSinais, 
        IGeradorDeDadosDeSinaisDinamicos
    {
        public double[][][] Entradas { get; private set; }

        private LinkedList<double[][]> entradas;
        private CaracteristicasSinalDinamico caracteristicas;

        public override int QuantidadeDeClassesPorSinal
        {
            get { return 1; }
        }

        public GeradorDeDadosDeSinaisDinamicos(IEnumerable<Sinal> sinais)
            :base(sinais)
        {
        }

        protected override void Inicializar(IEnumerable<Sinal> sinais)
        {
            entradas = new LinkedList<double[][]>();
            caracteristicas = new CaracteristicasSinalDinamico();
        }

        protected override void GerarEntradasESaidasParaAmostra(IList<Frame> amostra, LinkedList<int> saidas, int identificadorDoSinal)
        {
            entradas.AddLast(caracteristicas.DaAmostra(amostra));
            saidas.AddLast(identificadorDoSinal);
        }

        protected override void Finalizar()
        {
            Entradas = entradas.ToArray();
        }
    }
}