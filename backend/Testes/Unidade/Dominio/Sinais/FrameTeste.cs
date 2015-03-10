using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Sinais;
using System.Linq;
using Signa.Dominio.Sinais.Caracteristicas;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Sinais
{
    [TestClass]
    public class FrameTeste
    {
        [TestMethod]
        public void criando_um_frame_sem_maos()
        {
            var frame = new Frame
            {
                MaoEsquerda = null,
                MaoDireita = null
            };

            var valoresPadroes = Mao.Vazia();

            frame.MaoEsquerda.ToArray().Should().ContainInOrder(valoresPadroes.ToArray());
            frame.MaoDireita.ToArray().Should().ContainInOrder(valoresPadroes.ToArray());
        }
    }
}
