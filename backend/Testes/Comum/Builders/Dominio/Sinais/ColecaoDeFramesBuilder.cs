using System.Collections.Generic;
using Aplicacao.Dominio.Sinais;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class ColecaoDeFramesBuilder
    {
        private IList<Frame> frames = new []{ new FrameBuilder().Construir() };

        public ColecaoDeFramesBuilder ParaOIndiceComQuantidade(int indice, int quantidadeDeFrames = 2)
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