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
            throw new System.NotImplementedException();
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
            var framesData = new double[Frames.Count][];

            for (var i = 0; i < framesData.Length; i++)
            {
                framesData[i] = Frames[i].ToArray();
            }

            return framesData;
        }
    }
}