using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Caracteristicas;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Sinais.Dinamico
{
    [TestClass]
    public class FrameTeste
    {
        [TestMethod]
        public void criando_um_frame_com_a_mao_esquerda()
        {
            var maoEsquerda = DadaUmaMaoEsquerda();
            var maoDireita = Mao.Vazia();
            var frame = DadoUmFrameComMaos(maoEsquerda, null);

            var frameArray = frame.ToArray();

            DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(maoEsquerda, maoDireita, frameArray);
        }

        [TestMethod]
        public void criando_um_frame_com_a_mao_direita()
        {
            var maoEsquerda = Mao.Vazia();
            var maoDireita = DadaUmaMaoDireita();
            var frame = DadoUmFrameComMaos(null, maoDireita);

            var frameArray = frame.ToArray();

            DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(maoEsquerda, maoDireita, frameArray);
        }

        [TestMethod]
        public void criando_um_frame_com_duas_maos()
        {
            var maoEsquerda = DadaUmaMaoEsquerda();
            var maoDireita = DadaUmaMaoDireita();
            var frame = DadoUmFrameComMaos(maoEsquerda, maoDireita);

            var frameArray = frame.ToArray();

            DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(maoEsquerda, maoDireita, frameArray);
        }

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

        private Frame DadoUmFrameComMaos(Mao maoEsquerda, Mao maoDireita)
        {
            var frame = new FrameBuilder()
                .ComMaoEsquerda(maoEsquerda)
                .ComMaoDireita(maoDireita)
                .Construir();

            return frame;
        }

        private Mao DadaUmaMaoDireita()
        {
            var maoDireita = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.4, 0.5, 0.6 })
                .ComVetorNormalDaPalma(new[] { 0.0, 1.0, 1.0 })
                .Construir();
            return maoDireita;
        }

        private Mao DadaUmaMaoEsquerda()
        {
            var maoEsquerda = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.1, 0.2, 0.3 })
                .ComVetorNormalDaPalma(new[] { 1.0, 0.0, 1.0 })
                .Construir();
            return maoEsquerda;
        }

        private void DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(Mao maoEsquerda, Mao maoDireita, double[] frameArray)
        {
            var dadosDoFrameEsperados = maoEsquerda.ToArray().Concat(maoDireita.ToArray());

            frameArray.Should().HaveCount(dadosDoFrameEsperados.Count());
            frameArray.Should().ContainInOrder(dadosDoFrameEsperados);
        }
    }
}
