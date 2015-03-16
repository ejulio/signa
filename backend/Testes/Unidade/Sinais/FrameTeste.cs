using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Unidade.Sinais
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
