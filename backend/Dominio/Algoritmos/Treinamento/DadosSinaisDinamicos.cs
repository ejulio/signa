using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Treinamento
{
    public class DadosSinaisDinamicos : DadosAlgoritmoClassificacaoSinais, 
        IDadosSinaisDinamicos
    {
        public double[][][] CaracteristicasSinais { get; private set; }

        private LinkedList<double[][]> caracteristicasSinais;
        private CaracteristicasSinalDinamico caracteristicas;

        public override int QuantidadeClassesPorSinal
        {
            get { return 1; }
        }

        public DadosSinaisDinamicos(IEnumerable<Sinal> sinais)
            :base(sinais)
        {
        }

        protected override void Inicializar(IEnumerable<Sinal> sinais)
        {
            caracteristicasSinais = new LinkedList<double[][]>();
            caracteristicas = new CaracteristicasSinalDinamico();
        }

        protected override void GerarEntradasESaidasDaAmostra(IList<Frame> amostra, LinkedList<int> identificadoresSinais, int identificadorSinal)
        {
            caracteristicasSinais.AddLast(caracteristicas.DaAmostra(amostra));
            identificadoresSinais.AddLast(identificadorSinal);
        }

        protected override void Finalizar()
        {
            CaracteristicasSinais = caracteristicasSinais.ToArray();
        }
    }
}