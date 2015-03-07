using Signa.Domain.Signs.Dynamic;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs.Dynamic
{
    public class AmostraDeSinalBuilder
    {
        private IList<SignFrame> frames;

        public AmostraDeSinalBuilder ComFrames(IList<SignFrame> frames)
        {
            this.frames = frames;
            return this;
        }

        public AmostraDeSinal Construir()
        {
            return new AmostraDeSinal
            {
                Frames = frames
            };
        }
    }
}
