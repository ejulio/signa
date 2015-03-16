﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Sinais
{
    [TestClass]
    public class FrameTeste
    {
        [TestMethod]
        public void criando_um_frame()
        {
            var frame = new Frame();

            frame.MaoEsquerda.Should().NotBeNull();
            frame.MaoDireita.Should().NotBeNull();
        }

        [TestMethod]
        public void criando_um_frame_sem_maos()
        {
            var frame = new Frame
            {
                MaoEsquerda = null,
                MaoDireita = null
            };

            frame.MaoEsquerda.Should().NotBeNull();
            frame.MaoDireita.Should().NotBeNull();
        }
    }
}
