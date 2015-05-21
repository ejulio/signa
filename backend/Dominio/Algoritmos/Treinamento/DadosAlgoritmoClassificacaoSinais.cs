using Dominio.Sinais;
using Dominio.Sinais.Frames;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Treinamento
{
    public abstract class DadosAlgoritmoClassificacaoSinais : IDadosAlgoritmoClassificacaoSinais
    {
        public int[] IdentificadoresSinais { get; private set; }
        public int QuantidadeClasses { get; private set; }
        public abstract int QuantidadeClassesPorSinal { get; }

        private readonly IEnumerable<Sinal> sinais;
        private LinkedList<int> identificadoresSinais;

        protected DadosAlgoritmoClassificacaoSinais(IEnumerable<Sinal> sinais)
        {
            this.sinais = sinais;
            identificadoresSinais = new LinkedList<int>();
        }

        public void Processar()
        {
            var identificadorDoSinal = 0;
            QuantidadeClasses = 0;
            identificadoresSinais = new LinkedList<int>();

            Inicializar(sinais);

            foreach (var sinal in sinais)
            {
                foreach (var amostra in sinal.Amostras)
                    GerarEntradasESaidasDaAmostra(amostra, identificadoresSinais, identificadorDoSinal);

                sinal.IdNoAlgoritmo = identificadorDoSinal;
                identificadorDoSinal++;
                QuantidadeClasses += QuantidadeClassesPorSinal;
            }

            IdentificadoresSinais = identificadoresSinais.ToArray();
            Finalizar();
        }

        protected abstract void Inicializar(IEnumerable<Sinal>  sinais);
        protected abstract void GerarEntradasESaidasDaAmostra(IList<Frame> amostra, LinkedList<int> identificadoresSinais, int identificadorSinal);
        protected abstract void Finalizar();
    }
}