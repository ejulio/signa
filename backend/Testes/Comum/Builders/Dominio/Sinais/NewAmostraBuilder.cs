using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class NewAmostraBuilder
    {
        private IList<Frame> frames = new []{ new FrameBuilder().Construir() };

        public NewAmostraBuilder ComFrames(IList<Frame> frames)
        {
            this.frames = frames;
            return this;
        }

        public NewAmostraBuilder ParaOIndiceComQuantidade(int indice, int quantidadeDeFrames = 2)
        {
            frames = new Frame[quantidadeDeFrames];

            for (int i = 0; i < frames.Count; i++)
            {
                frames[i] = new FrameBuilder().ParaOIndice(indice).Construir();
            }

            return this;
        }


        public IList<Frame> Construir()
        {
            return frames;
        } 
    }
}