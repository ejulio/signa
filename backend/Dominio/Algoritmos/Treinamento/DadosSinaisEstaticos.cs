using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Treinamento
{
    public class DadosSinaisEstaticos : DadosAlgoritmoClassificacaoSinais, 
        IDadosSinaisEstaticos
    {
        public double[][] CaracteristicasSinais { get; private set; }
        
        private LinkedList<double[]> caracteristicasSinais;

        private CaracteristicasSinalEstatico caracteristicas;

        public override int QuantidadeClassesPorSinal
        {
            get { return 1; }
        }

        public DadosSinaisEstaticos(IEnumerable<Sinal> sinais)
            : base(sinais)
        {
        }

        protected override void Inicializar(IEnumerable<Sinal> sinais)
        {
            caracteristicasSinais = new LinkedList<double[]>();
            caracteristicas = new CaracteristicasSinalEstatico();
        }

        protected override void GerarEntradasESaidasDaAmostra(IList<Frame> amostra, LinkedList<int> identificadoresSinais, int identificadorSinal)
        {
            var caracteristicasDaAmostra = caracteristicas.DaAmostra(amostra);
            caracteristicasSinais.AddLast(caracteristicasDaAmostra);
            identificadoresSinais.AddLast(identificadorSinal);
        }

        protected override void Finalizar()
        {
            CaracteristicasSinais = caracteristicasSinais.ToArray();
        }
    }
}