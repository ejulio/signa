using System.Collections.Generic;

namespace Signa.Dominio.Sinais
{
    public class Amostra : IAmostraDeSinalEstatico, IAmostraDeSinalDinamico
    {
        public int QuantidadeDeFrames { get; private set; }

        public int QuantidadeDeCaracteristicas { get; private set; }

        private IList<Frame> frames; 
        public IList<Frame> Frames 
        { 
            get { return frames; }
            set
            {
                frames = value;
                if (frames != null && frames.Count > 0)
                {
                    QuantidadeDeFrames = frames.Count;
                    QuantidadeDeCaracteristicas = frames[0].ToArray().Length;
                }
                else
                {
                    QuantidadeDeFrames = 0;
                    QuantidadeDeCaracteristicas = 0;
                }
            } 
        }

        public Amostra()
        {
            Frames = new Frame[0];
        }

        double[] IAmostraDeSinalEstatico.ParaArray()
        {
            return Frames[0].ToArray();
        }

        public IAmostraDeSinalEstatico PrimeiroFrame()
        {
            return new Amostra
            {
                Frames = new []{ Frames[0] }
            };
        }

        public IAmostraDeSinalEstatico UltimoFrame()
        {
            return new Amostra
            {
                Frames = new[] { Frames[Frames.Count - 1] }
            };
        }

        double[][] IAmostraDeSinalDinamico.ParaArray()
        {
            var dadosDosFrames = new double[Frames.Count][];

            for (var i = 0; i < dadosDosFrames.Length; i++)
            {
                dadosDosFrames[i] = Frames[i].ToArray();
            }

            return dadosDosFrames;
        }
    }
}