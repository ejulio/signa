using System.Collections.Generic;

namespace Signa.Dominio.Sinais
{
    public class Amostra : IAmostraDeSinalEstatico, IAmostraDeSinalDinamico
    {
        public IList<Frame> Frames { get; set; }

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
            throw new System.NotImplementedException();
        }

        public IAmostraDeSinalEstatico UltimoFrame()
        {
            throw new System.NotImplementedException();
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