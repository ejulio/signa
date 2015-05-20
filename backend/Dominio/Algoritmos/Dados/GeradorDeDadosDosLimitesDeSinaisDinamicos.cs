using Dominio.Algoritmos.Caracteristicas;
using Dominio.Sinais;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais.Frames;

namespace Dominio.Algoritmos.Dados
{
    public class GeradorDeDadosDosLimitesDeSinaisDinamicos : GeradorDeDadosParaAlgoritmoDeReconhecimentoDeSinais,
        IGeradorDeDadosDeSinaisEstaticos
    {
        public double[][] Entradas { get; private set; }

        private LinkedList<double[]> entradas;
        private CaracteristicasSinalEstaticoComTipoFrame caracteristicasComTipoFrame;

        private int quantidadeDeSinais;

        public override int QuantidadeDeClassesPorSinal
        {
            get { return 2; }
        }

        public GeradorDeDadosDosLimitesDeSinaisDinamicos(IEnumerable<Sinal> sinais)
            : base(sinais)
        {
        }

        protected override void Inicializar(IEnumerable<Sinal> sinais)
        {
            quantidadeDeSinais = sinais.Count();
            entradas = new LinkedList<double[]>();
            var geradorDeCaracteristicas = new CaracteristicasSinalEstatico();
            caracteristicasComTipoFrame = new CaracteristicasSinalEstaticoComTipoFrame(geradorDeCaracteristicas);
        }

        protected override void GerarEntradasESaidasParaAmostra(IList<Frame> amostra, LinkedList<int> saidas, int identificadorDoSinal)
        {
            var quantidadeFramesQueRepresentamPrimeiroFrame = amostra.Count / 2;
            caracteristicasComTipoFrame.PrimeiroFrame = amostra.First();
            
            GerarEntradasSaidasQueRepresentamPrimeiroFrame(amostra, saidas, identificadorDoSinal, quantidadeFramesQueRepresentamPrimeiroFrame);
            GerarEntradasSaidasQueRepresentamUltimoFrame(amostra, saidas, identificadorDoSinal, quantidadeFramesQueRepresentamPrimeiroFrame);
        }

        private void GerarEntradasSaidasQueRepresentamPrimeiroFrame(IList<Frame> amostra, LinkedList<int> saidas, 
            int indice, int quantidadeFramesQueRepresentamPrimeiroFrame)
        {
            var amostraDoFrame = new Frame[1];
            caracteristicasComTipoFrame.TipoFrame = TipoFrame.Primeiro;
            for (int i = 0; i < quantidadeFramesQueRepresentamPrimeiroFrame; i++)
            {
                amostraDoFrame[0] = amostra[i];
                entradas.AddLast(caracteristicasComTipoFrame.DaAmostra(amostraDoFrame));
                saidas.AddLast(indice);
            }
        }

        private void GerarEntradasSaidasQueRepresentamUltimoFrame(IList<Frame> amostra, LinkedList<int> saidas, 
            int indice, int quantidadeFramesQueRepresentamPrimeiroFrame)
        {
            var amostraDoFrame = new Frame[1];
            caracteristicasComTipoFrame.TipoFrame = TipoFrame.Ultimo;
            for (int i = quantidadeFramesQueRepresentamPrimeiroFrame; i < amostra.Count; i++)
            {
                amostraDoFrame[0] = amostra[i];
                entradas.AddLast(caracteristicasComTipoFrame.DaAmostra(amostraDoFrame));
                saidas.AddLast(indice + quantidadeDeSinais);
            }
        }

        protected override void Finalizar()
        {
            Entradas = entradas.ToArray();
        }
    }
}