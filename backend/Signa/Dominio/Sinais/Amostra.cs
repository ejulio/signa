using System.Collections.Generic;

namespace Signa.Dominio.Sinais
{
    public class Amostra
    {
        public IList<Frame> Frames { get; set; }

        public Amostra()
        {
            Frames = new Frame[0];
        }

        public double[][] ToArray()
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