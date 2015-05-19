using Dominio.Sinais.Maos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Unidade.Sinais.Maos
{
    [TestClass]
    public class MaoTeste
    {
        [TestMethod]
        public void criando_uma_mao()
        {
            var mao = new Mao();

            DeveTerOsValoresPadroes(mao);
        }

        [TestMethod]
        public void criando_uma_mao_com_valores_null()
        {
            var mao = new Mao
            {
                Dedos = null,
                Direcao = null,
                VetorNormalDaPalma = null
            };

            DeveTerOsValoresPadroes(mao);
        }

        private void DeveTerOsValoresPadroes(Mao mao)
        {
            mao.Direcao.Should().NotBeNull();
            mao.Direcao.Should().HaveCount(3);
            mao.VetorNormalDaPalma.Should().NotBeNull();
            mao.VetorNormalDaPalma.Should().HaveCount(3);
            mao.Dedos.Should().NotBeNull();
            mao.Dedos.Should().HaveCount(5);
            foreach (var dedo in mao.Dedos)
            {
                dedo.Should().NotBeNull();
            }
        }
    }
}