﻿using Signa.Dominio.Sinais;
using System.Collections.Generic;

namespace Testes.Comum.Builders.Dominio.Sinais
{
    public class AmostraBuilder
    {
        private IList<Frame> frames = new List<Frame>();

        public AmostraBuilder ComFrames(IList<Frame> frames)
        {
            this.frames = frames;
            return this;
        }

        public AmostraBuilder ParaOIndiceComQuantidade(int indice, int quantidadeDeFrames = 2)
        {
            frames = new Frame[quantidadeDeFrames];

            for (int i = 0; i < frames.Count; i++)
            {
                frames[i] = new FrameBuilder().ParaOIndice(indice).Construir();
            }
            
            return this;
        }

        public IAmostraDeSinalDinamico ConstruirAmostraDinamica()
        {
            return Construir();
        }

        public IAmostraDeSinalEstatico ConstruirAmostraEstatica()
        {
            return Construir();
        }

        public Amostra Construir()
        {
            return new Amostra
            {
                Frames = frames
            };
        }
    }
}