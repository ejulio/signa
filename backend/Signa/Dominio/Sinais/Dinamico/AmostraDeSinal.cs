using System.Collections.Generic;

namespace Signa.Dominio.Sinais.Dinamico
{
    public class AmostraDeSinal
    {
        public IList<FrameDeSinal> Frames { get; set; }

        public AmostraDeSinal()
        {
            Frames = new FrameDeSinal[0];
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