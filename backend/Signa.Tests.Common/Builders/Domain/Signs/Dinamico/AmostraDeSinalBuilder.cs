using Signa.Domain.Signs.Dynamic;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders.Domain.Signs.Dinamico
{
    public class AmostraDeSinalBuilder
    {
        private IList<FrameDeSinal> frames;

        public AmostraDeSinalBuilder ComFrames(IList<FrameDeSinal> frames)
        {
            this.frames = frames;
            return this;
        }

        public AmostraDeSinalBuilder ParaOIndiceComQuantidade(int indice, int quantidadeDeFrames = 2)
        {
            frames = new FrameDeSinal[quantidadeDeFrames];

            for (int i = 0; i < frames.Count; i++)
            {
                frames[i] = new FrameDeSinalBuilder().ParaOIndice(indice).Construir();
            }
            
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
