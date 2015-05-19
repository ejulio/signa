using Dominio.Sinais;
using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Sinais.Frames;
using Dominio.Sinais.Maos;
using Dominio.Util.Matematica;

namespace Dominio.Algoritmos.Caracteristicas
{
    public class GeradorDeCaracteristicasDeSinalDinamico : GeradorDeCaracteristicasDeFrame, IGeradorDeCaracteristicasDeSinalDinamico
    {
        public double[][] ExtrairCaracteristicasDaAmostra(IList<Frame> frames)
        {
            var caracteristicasDosFrames = new double[frames.Count][];
            double[] posicaoDaMaoDireitaNoPrimeiroFrame = PosicaoDaPalmaNoPrimeiroFrame(frames[0].MaoDireita);
            double[] posicaoDaMaoEsquerdaNoPrimeiroFrame = PosicaoDaPalmaNoPrimeiroFrame(frames[0].MaoEsquerda);

            for (int i = 0; i < frames.Count; i++)
                caracteristicasDosFrames[i] = ExtrairCaracteristicasComDistanciaDaPalma(frames[i], i, posicaoDaMaoDireitaNoPrimeiroFrame, posicaoDaMaoEsquerdaNoPrimeiroFrame);
            
            return caracteristicasDosFrames;
        }

        private double[] PosicaoDaPalmaNoPrimeiroFrame(Mao mao)
        {
            if (mao == null)
                return PosicaoOrigem();

            return mao.PosicaoDaPalma;
        }

        private double[] ExtrairCaracteristicasComDistanciaDaPalma(Frame frame, int indice, double[] posicaoDaMaoDireitaNoPrimeiroFrame, double[] posicaoDaMaoEsquerdaNoPrimeiroFrame)
        {
            var caracteristicas = ExtrairCaracteristicasDoFrame(frame);
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
                    .Concat(DistanciaDaPalmaDaMaoEmRelacaoAoPrimeiroFrame(frame.MaoDireita, posicaoDaMaoDireitaNoPrimeiroFrame))
                    .Concat(DistanciaDaPalmaDaMaoEmRelacaoAoPrimeiroFrame(frame.MaoEsquerda, posicaoDaMaoEsquerdaNoPrimeiroFrame))
                    .ToArray();
            }
            return caracteristicas;
        }

        private bool EhPrimeiroFrame(int indice)
        {
            return indice == 0;
        }

        private double[] DistanciaDaPalmaDaMaoEmRelacaoAoPrimeiroFrame(Mao mao, double[] posicaoDaPalmaDaMaoNoPrimeiroFrame)
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