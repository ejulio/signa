using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Treinamento
{
    public class DadosFramesSinaisDinamicos : DadosAlgoritmoClassificacaoSinais, IDadosSinaisEstaticos
    {
        public double[][] CaracteristicasSinais { get; private set; }

        private LinkedList<double[]> caracteristicasSinais;
        private CaracteristicasSinalEstaticoComTipoFrame caracteristicasComTipoFrame;

        private int quantidadeDeSinais;

        public override int QuantidadeClassesPorSinal
        {
            get { return 2; }
        }

        public DadosFramesSinaisDinamicos(IEnumerable<Sinal> sinais)
            : base(sinais)
        {
        }

        protected override void Inicializar(IEnumerable<Sinal> sinais)
        {
            quantidadeDeSinais = sinais.Count();
            caracteristicasSinais = new LinkedList<double[]>();
            var geradorDeCaracteristicas = new CaracteristicasSinalEstatico();
            caracteristicasComTipoFrame = new CaracteristicasSinalEstaticoComTipoFrame(geradorDeCaracteristicas);
        }

        protected override void GerarEntradasESaidasDaAmostra(IList<Frame> amostra, LinkedList<int> identificadoresSinais, int identificadorSinal)
        {
            var quantidadeFramesQueRepresentamPrimeiroFrame = amostra.Count / 2;
            caracteristicasComTipoFrame.PrimeiroFrame = amostra.First();
            
            GerarEntradasESaidasPrimeiroFrame(amostra, identificadoresSinais, identificadorSinal, quantidadeFramesQueRepresentamPrimeiroFrame);
            GerarEntradasESaidasUltimoFrame(amostra, identificadoresSinais, identificadorSinal, quantidadeFramesQueRepresentamPrimeiroFrame);
        }

        private void GerarEntradasESaidasPrimeiroFrame(IList<Frame> amostra, LinkedList<int> saidas, 
            int indice, int quantidadeFramesQueRepresentamPrimeiroFrame)
        {
            var amostraDoFrame = new Frame[1];
            caracteristicasComTipoFrame.TipoFrame = TipoFrame.Primeiro;
            for (int i = 0; i < quantidadeFramesQueRepresentamPrimeiroFrame; i++)
            {
                amostraDoFrame[0] = amostra[i];
                caracteristicasSinais.AddLast(caracteristicasComTipoFrame.DaAmostra(amostraDoFrame));
                saidas.AddLast(indice);
            }
        }

        private void GerarEntradasESaidasUltimoFrame(IList<Frame> amostra, LinkedList<int> saidas, 
            int indice, int quantidadeFramesQueRepresentamPrimeiroFrame)
        {
            var amostraDoFrame = new Frame[1];
            caracteristicasComTipoFrame.TipoFrame = TipoFrame.Ultimo;
            for (int i = quantidadeFramesQueRepresentamPrimeiroFrame; i < amostra.Count; i++)
            {
                amostraDoFrame[0] = amostra[i];
                caracteristicasSinais.AddLast(caracteristicasComTipoFrame.DaAmostra(amostraDoFrame));
                saidas.AddLast(indice + quantidadeDeSinais);
            }
        }

        protected override void Finalizar()
        {
            CaracteristicasSinais = caracteristicasSinais.ToArray();
        }
    }
}