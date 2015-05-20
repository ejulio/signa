using Dominio.Sinais.Frames;
using Dominio.Sinais.Maos;
using Dominio.Util.Matematica;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class CaracteristicasSinalDinamico : CaracteristicasFrame, ICaracteristicasSinalDinamico
    {
        public double[][] DaAmostra(IList<Frame> amostra)
        {
            var caracteristicasDosFrames = new double[amostra.Count][];
            var posicaoMaoDireitaPrimeiroFrame = PosicaoDaPalmaPrimeiroFrame(amostra[0].MaoDireita);
            var posicaoMaoEsquerdaPrimeiroFrame = PosicaoDaPalmaPrimeiroFrame(amostra[0].MaoEsquerda);

            for (int i = 0; i < amostra.Count; i++)
                caracteristicasDosFrames[i] = CaracteristicasDoFrameComDistanciaDaPalma(amostra[i], i, posicaoMaoDireitaPrimeiroFrame, posicaoMaoEsquerdaPrimeiroFrame);
            
            return caracteristicasDosFrames;
        }

        private double[] PosicaoDaPalmaPrimeiroFrame(Mao mao)
        {
            if (mao == null)
                return PosicaoOrigem();

            return mao.PosicaoDaPalma;
        }

        private double[] CaracteristicasDoFrameComDistanciaDaPalma(Frame frame, int indice, double[] posicaoDaMaoDireitaPrimeiroFrame, double[] posicaoDaMaoEsquerdaPrimeiroFrame)
        {
            var caracteristicas = CaracteristicasDoFrame(frame);
            if (EhPrimeiroFrame(indice))
            {
                caracteristicas = caracteristicas
                    .Concat(PosicaoOrigem())
                    .Concat(PosicaoOrigem())
                    .ToArray();
            }
            else
            {
                caracteristicas = caracteristicas
                    .Concat(DistanciaDaMaoEmRelacaoAoPrimeiroFrame(frame.MaoDireita, posicaoDaMaoDireitaPrimeiroFrame))
                    .Concat(DistanciaDaMaoEmRelacaoAoPrimeiroFrame(frame.MaoEsquerda, posicaoDaMaoEsquerdaPrimeiroFrame))
                    .ToArray();
            }
            return caracteristicas;
        }

        private bool EhPrimeiroFrame(int indice)
        {
            return indice == 0;
        }

        private double[] DistanciaDaMaoEmRelacaoAoPrimeiroFrame(Mao mao, double[] posicaoDaPalmaDaMaoNoPrimeiroFrame)
        {
            if (mao == null)
                return PosicaoOrigem();

            return mao.PosicaoDaPalma.Subtrair(posicaoDaPalmaDaMaoNoPrimeiroFrame).Normalizado();
        }

        private double[] PosicaoOrigem()
        {
            return new[] { 0.0, 0.0, 0.0 };
        }
    }
}