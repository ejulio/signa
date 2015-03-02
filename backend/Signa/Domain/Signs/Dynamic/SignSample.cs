using System.Collections.Generic;

namespace Signa.Domain.Signs.Dynamic
{
    public class SignSample
    {
        public IList<SignFrame> Frames { get; set; }

        public SignSample()
        {
            Frames = new SignFrame[0];
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